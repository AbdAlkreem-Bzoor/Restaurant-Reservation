CREATE OR ALTER PROCEDURE sp_CustomersWithPartySizeAbove (
@Size INT
)
AS
BEGIN
     SELECT C.CustomerId, CONCAT([First Name], ' ', [Last Name]) AS [Name], Email, [Phone Number]
	 FROM Customers C JOIN Reservations R ON C.CustomerId = R.CustomerId
	 WHERE [Party Size] >= @Size;
END;