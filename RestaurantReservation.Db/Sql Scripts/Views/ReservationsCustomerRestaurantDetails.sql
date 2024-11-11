CREATE VIEW ReservationsCustomerRestaurantDetails AS 
SELECT C.CustomerId, CONCAT([First Name], ' ', [Last Name]) AS [Name], Email, C.[Phone Number],
X.ReservationId, X.[Reservation Date], X.TableId, X.[Party Size], Y.RestaurantId, Y.[Name] AS [Restaurant Name],
Y.[Address], Y.[Phone Number] AS [Restaurant Phone Number]
FROM Customers C JOIN Reservations X ON C.CustomerId = X.CustomerId
JOIN Restaurants Y ON X.RestaurantId = Y.RestaurantId;