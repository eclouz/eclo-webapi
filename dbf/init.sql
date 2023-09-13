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
