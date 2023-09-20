create or replace procedure transfer(sender_card_number text, receiver_card_number text, amount double precision)
language plpgsql
as
$$
	declare
		minimum_balance double precision := 1;
		maximum_balance double precision := 100000;
		transfer_tax_percentage double precision := 1.5;
		
		transfer_tax double precision;
		sender_card user_cards%rowtype;
		receiver_card user_cards%rowtype;
	begin
		-- check amount
		if amount > maximum_balance then
			raise exception 'amount must be less than %', maximum_balance;
		elsif amount < minimum_balance then
			raise exception 'amount must be more than %', minimum_balance;
		end if;
		
		-- user exists check
		select * from user_cards into sender_card where card_number = sender_card_number limit 1;
		if sender_card.id is null then
			raise exception 'sender not found with this card number %!', sender_card_number;
		end if;
		
		select into receiver_card * from user_cards where card_number = receiver_card_number limit 1;
		if receiver_card.id is null then
			raise exception 'receiver not found with this card number %!', receiver_card_number;
		end if;
		
		-- check sender user balance
		transfer_tax = amount * transfer_tax_percentage / 100;
		if sender_card.balance < amount + transfer_tax then 
			raise exception 'sender card balance is not enough to transfer in this card number %!',sender_card_number;
		end if;
		
		-- transfer
		update user_cards set balance = balance - amount - transfer_tax where user_id = sender_card.user_id;
		update user_cards set balance = balance + amount where user_id = receiver_card.user_id;
		insert into public.user_transactions(user_id, sender_card_number, receiver_card_number, required_amount, is_transfered, status, created_at, updated_at) 
			values (sender_card.user_id, sender_card.card_number, receiver_card.card_number, 0 - amount - transfer_tax, true, 'successfull', now(), now());		
		insert into public.user_transactions(user_id, sender_card_number, receiver_card_number, required_amount, is_transfered, status, created_at, updated_at) 
			values (receiver_card.user_id, sender_card.card_number, receiver_card.card_number, amount, true, 'successfull', now(), now());
		
		commit;
		
		exception
			when others then
				rollback;
	end;
$$;

call transfer('8600 3129 1066 2275','8600 3129 4567 7890',10);