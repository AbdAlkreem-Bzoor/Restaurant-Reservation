CREATE OR ALTER FUNCTION fn_CalculateTotalRevenueByRestaurant
(
@RestaurantId INT
)
RETURNS DECIMAL(15, 2)
AS
BEGIN
     DECLARE @Result DECIMAL(15, 2) = 0.00;
	 SELECT @Result = [Revenue]
	 FROM (
	 SELECT SUM([Total Amount]) AS [Revenue]
	 FROM Restaurants R JOIN Employees E ON R.RestaurantId = E.RestaurantId
	 JOIN Orders O ON E.EmployeeId = O.EmployeeId
	 WHERE R.RestaurantId = @RestaurantId) AS X;
     RETURN @Result;
END;

