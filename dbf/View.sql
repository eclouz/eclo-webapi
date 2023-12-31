
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
 SELECT DISTINCT ON (product_admin_view.product_id) product_admin_view.product_id,
    product_admin_view.product_detail_id,
    product_admin_view.product_image_path,
    product_admin_view.product_name,
    product_admin_view.product_category_name,
    product_admin_view.product_likes,
    product_admin_view.product_price,
    product_admin_view.product_last_update
   FROM product_admin_view;

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