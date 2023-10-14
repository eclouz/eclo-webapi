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


CREATE VIEW admin_user_view AS
 SELECT users.id,
    users.first_name,
    users.last_name,
    users.phone_number,
    users.phone_number_confirmed,
    users.image_path,
    users.passport_serial_number,
    users.birth_date,
    users.region,
    users.district,
    users.address,
    users.created_at,
    users.updated_at
   FROM users;

CREATE VIEW product_comment_view AS
 SELECT product_comments.id,
    product_comments.comment,
    product_comments.created_at,
    users.first_name,
    users.last_name,
    users.image_path
   FROM product_comments
     JOIN users ON users.id = product_comments.user_id;

CREATE VIEW product_detail_fashion_view AS
 SELECT product_detail_fashions.id,
    product_detail_fashions.image_path
   FROM product_detail_fashions;

CREATE VIEW product_view AS
 SELECT products.id,
    products.name AS product_name,
    brands.name AS brand_name,
    product_details.image_path AS product_image_path,
    product_details.color AS product_color,
    products.unit_price AS product_price,
    discounts.percentage AS product_discount,
    products.description AS product_description,
    product_detail_sizes.size AS product_size,
    user_product_likes.is_liked AS product_liked
   FROM products
     LEFT JOIN brands ON products.brand_id = brands.id
     LEFT JOIN product_details ON products.id = product_details.product_id
     LEFT JOIN product_detail_sizes ON product_details.id = product_detail_sizes.product_detail_id
     LEFT JOIN product_discounts ON products.id = product_discounts.product_id
     LEFT JOIN discounts ON product_discounts.discount_id = discounts.id
     LEFT JOIN user_product_likes ON products.id = user_product_likes.product_id;

CREATE VIEW user_view AS
 SELECT users.id,
    users.first_name,
    users.last_name,
    users.phone_number,
    users.phone_number_confirmed,
    users.image_path,
    users.passport_serial_number,
    users.birth_date,
    users.region,
    users.district,
    users.address
   FROM users;

CREATE VIEW subcategory_viewmodel AS
 SELECT subcategories.id,
    subcategories.category_id,
    subcategories.name,
    categories.name AS category_name,
    subcategories.created_at,
    subcategories.updated_at
   FROM sub_categories subcategories
     JOIN categories ON subcategories.category_id = categories.id;

CREATE VIEW order_view AS
 SELECT orders.id AS order_id,
    products.name AS product_name,
    discounts.percentage AS discount_percentage,
    order_details.quantity,
    order_details.total_price,
    orders.is_contracted,
    orders.is_paid,
    orders.status AS order_status
   FROM orders
     JOIN order_details ON orders.id = order_details.order_id
     JOIN product_discounts ON order_details.product_discount_id = product_discounts.id
     LEFT JOIN products ON product_discounts.product_id = products.id
     LEFT JOIN discounts ON product_discounts.discount_id = discounts.id;

CREATE VIEW product_admin_view AS
    SELECT products.id AS product_id,
    products.name AS product_name,
    ( SELECT categories.name
           FROM sub_categories
             LEFT JOIN categories ON categories.id = sub_categories.category_id
         LIMIT 1) AS product_category_name,
    ( SELECT count(*) AS count
           FROM user_product_likes
          WHERE user_product_likes.product_id = products.id
         LIMIT 1) AS product_likes,
    products.unit_price AS product_price,
    products.updated_at AS product_last_update
   FROM products;

CREATE VIEW product_result_view AS
SELECT DISTINCT ON (product_admin_view.product_id)
    product_admin_view.product_id AS product_detail_id,
    product_details.image_path AS product_image_path,
    product_admin_view.product_name AS product_name,
    (
        SELECT categories.name
        FROM sub_categories
        LEFT JOIN categories ON categories.id = sub_categories.category_id
        LIMIT 1
    ) AS product_category_name,
    (
        SELECT COUNT(*) AS count
        FROM user_product_likes
        WHERE user_product_likes.product_id = product_admin_view.product_id
        LIMIT 1
    ) AS product_likes,
    product_admin_view.product_price AS product_price,
    product_admin_view.product_last_update AS product_last_update
FROM product_admin_view
LEFT JOIN product_details ON product_admin_view.product_id = product_details.product_id;

CREATE VIEW product_detail_view AS
 SELECT product_details.id,
    products.id AS product_id,
    product_details.image_path,
    product_details.color
   FROM product_details
     LEFT JOIN products ON products.id = product_details.product_id;

CREATE VIEW product_admin_detail_fashion_view AS
 SELECT product_detail_fashions.id,
    product_details.id AS product_detail_id,
    product_detail_fashions.image_path
   FROM product_detail_fashions
     LEFT JOIN product_details ON product_details.id = product_detail_fashions.product_detail_id;

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
	'avatars\avatar.png', 
	'AC3028803',
	'2004-07-30', 
	'Uzbekistan', 
	'Jizzax', 
	'Jizzax shahar', 
	now(),
	now());


