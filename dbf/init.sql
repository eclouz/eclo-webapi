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
	category_id bigint references categories(id),
	name varchar(50) not null,
	created_at timestamp without time zone default now(),
	updated_at timestamp without time zone default now()
);

create table products (
	id bigint generated always as identity primary key,
	brand_id bigint references brands(id),
	sub_category_id bigint references sub_categories(id),
	name varchar(50) not null,
	unit_price double precision not null,
	description text,
	created_at timestamp without time zone default now(),
	updated_at timestamp without time zone default now()
);

create table user_product_likes (
	id bigint generated always as identity primary key,
	user_id bigint references users(id),
	product_id bigint references products(id),
	is_liked boolean not null,
	created_at timestamp without time zone default now(),
	updated_at timestamp without time zone default now()
);

create table product_comments (
	id bigint generated always as identity primary key,
	user_id bigint references users(id),
	product_id bigint references products(id),
	reply_comment_id bigint references product_comments(id),
	comment text,
	is_edited boolean not null,
	created_at timestamp without time zone default now(),
	updated_at timestamp without time zone default now()
);

create table product_details (
	id bigint generated always as identity primary key,
	product_id bigint references products(id),
	image_path text not null,
	color text,
	created_at timestamp without time zone default now(),
	updated_at timestamp without time zone default now()
);

create table product_detail_sizes (
	id bigint generated always as identity primary key,
	product_detail_id bigint references product_details(id),
	size text,
	quantity integer not null,
	created_at timestamp without time zone default now(),
	updated_at timestamp without time zone default now()
);

create table product_detail_fashions (
	id bigint generated always as identity primary key,
	product_detail_id bigint references product_details(id),
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
	product_id bigint references products(id),
	discount_id bigint references discounts(id),
	description text,
	start_at timestamp without time zone not null,
	end_at timestamp without time zone not null,
	created_at timestamp without time zone default now(),
	updated_at timestamp without time zone default now()
);