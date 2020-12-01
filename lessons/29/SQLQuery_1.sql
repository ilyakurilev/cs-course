WITH AllOrdersWithTotalSum([CustomerId], [Year], [Total Price]) AS 
(
    SELECT
    O.CustomerId,
    YEAR(O.OrderDate),
    SUM(P.Price * OL.[Count]) * (1 - ISNULL(O.Discount, 0)) AS 'Total Price'
FROM [Order] O
JOIN [OrderLine] OL ON O.Id = OL.OrderId
JOIN [Product] P ON OL.ProductId = P.Id
GROUP BY O.CustomerId, YEAR(O.OrderDate), O.Discount
),
TotalYearSum([CustomerId], [Year], [YearSum]) AS
(
    SELECT
        [CustomerId],
        [Year],
        SUM([Total Price])
    FROM AllOrdersWithTotalSum
    GROUP BY [Year], [CustomerId]
),
MaxTotalYearSum([Year], [YearSum]) AS
(
    SELECT
        [Year],
        MAX(YearSum)
    FROM TotalYearSum
    GROUP BY [Year]
)
SELECT
    MTYS.[Year],
    TYS.CustomerId,
    C.Name,
    MTYS.YearSum
FROM MaxTotalYearSum MTYS
JOIN TotalYearSum TYS ON MTYS.YearSum = TYS.YearSum
JOIN [Customer] C ON TYS.CustomerId = C.Id
GO