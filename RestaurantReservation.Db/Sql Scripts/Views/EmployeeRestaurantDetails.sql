CREATE VIEW EmployeeRestaurantDetails AS 
SELECT E.EmployeeId, CONCAT([First Name], ' ', [Last Name]) AS [Name], E.Position,
R.RestaurantId, R.[Name] AS [Restaurant Name], R.[Address], R.[Phone Number]
FROM Restaurants R JOIN Employees E ON R.RestaurantId = E.RestaurantId;