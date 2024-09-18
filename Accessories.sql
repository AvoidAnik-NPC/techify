CREATE PROCEDURE Accessoriesfiltering
AS
BEGIN
    SELECT * 
    FROM Product 
    INNER JOIN Category ON Product.CategoryID = Category.CategoryID 
    WHERE Category.CategoryName = 'Accessories';
END;
