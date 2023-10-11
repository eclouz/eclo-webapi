create table users (
	id bigint generated always as identity primary key,
	first_name varchar(50) not null,
	last_name varchar(50) not null,
	phone_number varchar(13) not null,
	phone_number_confirmed bool default false,
	password_hash text not null,
	salt text not null,
	image_path text,
	passport_serial_number varchar(9),
	birth_date timestamp without time zone default now(),
	region text,
	district text,
	address text,
	created_at timestamp without time zone default now(),
	updated_at timestamp without time zone default now()
);

create table admins (
	id bigint generated always as identity primary key,
	first_name varchar(50) not null,
	last_name varchar(50) not null,
	phone_number varchar(13) not null,
	phone_number_confirmed bool default false,
	password_hash text not null,
	salt text not null,
	image_path text,
	passport_serial_number varchar(9),
	birth_date timestamp without time zone default now(),
	region text,
	district text,
	address text,
	created_at timestamp without time zone default now(),
	updated_at timestamp without time zone default now()
);

create table heads (
	id bigint generated always as identity primary key,
	first_name varchar(50) not null,
	last_name varchar(50) not null,
	phone_number varchar(13) not null,
	phone_number_confirmed bool default false,
	password_hash text not null,
	salt text not null,
	image_path text,
	passport_serial_number varchar(9),
	birth_date timestamp without time zone default now(),
	region text,
	district text,
	address text,
	created_at timestamp without time zone default now(),
	updated_at timestamp without time zone default now()
);

create table categories (
	id bigint generated always as identity primary key,
	name varchar(50) not null,
	description text,
	created_at timestamp without time zone default now(),
	updated_at timestamp without time zone default now()
);

create table brands (
	id bigint generated always as identity primary key,
	name varchar(50) not null,
	brand_icon_path text,
	created_at timestamp without time zone default now(),
	updated_at timestamp without time zone default now()
);

create table sub_categories (
	id bigint generated always as identity primary key,
	category_id bigint references categories(id) ON DELETE CASCADE,
	name varchar(50) not null,
	created_at timestamp without time zone default now(),
	updated_at timestamp without time zone default now()
);

create table products (
	id bigint generated always as identity primary key,
	brand_id bigint references brands(id) ON DELETE CASCADE,
	sub_category_id bigint references sub_categories(id) ON DELETE CASCADE,
	name varchar(50) not null,
	unit_price double precision not null,
	description text,
	created_at timestamp without time zone default now(),
	updated_at timestamp without time zone default now()
);

create table user_product_likes (
	id bigint generated always as identity primary key,
	user_id bigint references users(id) ON DELETE CASCADE,
	product_id bigint references products(id) ON DELETE CASCADE,
	is_liked boolean not null,
	created_at timestamp without time zone default now(),
	updated_at timestamp without time zone default now()
);

create table product_comments (
	id bigint generated always as identity primary key,
	user_id bigint references users(id) ON DELETE CASCADE,
	product_id bigint references products(id) ON DELETE CASCADE,
	reply_comment_id bigint references product_comments(id) ON DELETE CASCADE,
	comment text,
	is_edited boolean not null,
	created_at timestamp without time zone default now(),
	updated_at timestamp without time zone default now()
);

create table product_details (
	id bigint generated always as identity primary key,
	product_id bigint references products(id) ON DELETE CASCADE,
	image_path text not null,
	color text,
	created_at timestamp without time zone default now(),
	updated_at timestamp without time zone default now()
);

create table product_detail_sizes (
	id bigint generated always as identity primary key,
	product_detail_id bigint references product_details(id) ON DELETE CASCADE,
	size text,
	quantity integer not null,
	created_at timestamp without time zone default now(),
	updated_at timestamp without time zone default now()
);

create table product_detail_fashions (
	id bigint generated always as identity primary key,
	product_detail_id bigint references product_details(id) ON DELETE CASCADE,
	image_path text not null,
	created_at timestamp without time zone default now(),
	updated_at timestamp without time zone default now()
);

create table discounts (
	id bigint generated always as identity primary key,
	name varchar(50) not null,
	percentage smallint,
	description text,
	created_at timestamp without time zone default now(),
	updated_at timestamp without time zone default now()
);

create table product_discounts (
	id bigint generated always as identity primary key,
	product_id bigint references products(id) ON DELETE CASCADE,
	discount_id bigint references discounts(id) ON DELETE CASCADE,
	description text,
	start_at timestamp without time zone not null,
	end_at timestamp without time zone not null,
	created_at timestamp without time zone default now(),
	updated_at timestamp without time zone default now()
);

create table orders (
	id bigint generated always as identity primary key,
	user_id bigint references users(id) ON DELETE CASCADE,
	products_price double precision not null,
	status text not null,
	description text,
	is_contracted boolean not null,
	is_paid boolean not null,
	payment_type text not null,
	created_at timestamp without time zone default now(),
	updated_at timestamp without time zone default now()
);

create table order_details (
	id bigint generated always as identity primary key,
	order_id bigint references orders(id) ON DELETE CASCADE,
	product_discount_id bigint references product_discounts(id) ON DELETE CASCADE,
	quantity integer not null,
	price double precision not null,
	discount_price double precision not null,
	total_price double precision not null,
	created_at timestamp without time zone default now(),
	updated_at timestamp without time zone default now()
);

create table payments (
	id bigint generated always as identity primary key,
	order_detail_id bigint references order_details(id) ON DELETE CASCADE,
	transaction_status text not null,
	description text,
	created_at timestamp without time zone default now(),
	updated_at timestamp without time zone default now()
);

ALTER DATABASE "eclo-db"
SET TIMEZONE TO 'Asia/Tashkent';


create table user_cards (
	id bigint generated always as identity primary key,
	user_id bigint references users(id) ON DELETE CASCADE not null,
	card_holder_name text not null,
	card_number varchar(19) not null unique,
	balance double precision not null,
	pin_code int not null,
	expired_month int not null,
	expired_year int not null,
	is_active bool default false,
	created_at timestamp without time zone default now(),
	updated_at timestamp without time zone default now()
);

create table user_transactions (
	id bigint generated always as identity primary key,
	user_id bigint references users(id) ON DELETE CASCADE not null,
	sender_card_number varchar(19) not null,
	receiver_card_number varchar(19) not null,
	required_amount double precision not null,
	is_transfered bool default false,
	status text not null, -- waiting, inprocess, successful, cancelled
	created_at timestamp without time zone default now(),
	updated_at timestamp without time zone default now()
);

INSERT INTO public.heads(
	first_name, 
	last_name, 
	phone_number, 
	phone_number_confirmed, 
	password_hash, 
	salt, 
	image_path, 
	passport_serial_number, 
	birth_date, 
	region, 
	district, 
	address, 
	created_at, 
	updated_at)
	VALUES ('Javohir', 
	'Ergashev', 
	'+998902295616', 
	true, 
	'$2a$11$6GOEOR47yjo1xP7ksfmz3ehXbwZqTJe1KlnNV0l3mKtxblUgXUJyS', 
	'8c76c930-735f-4340-923b-e3454e10f586', 
	'Avatars\avatar.png', 
	'AC3028803',
	'2004-07-30', 
	'Uzbekistan', 
	'Jizzax', 
	'Jizzax shahar', 
	now(),
	now());


