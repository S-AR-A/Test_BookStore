/* goal: name and purchased books' titles of customers who didn't have the best selling book in their orders*/
USE [LibraryDb]
go

select CustomerName, Books.Title
from dbo.Orders a 
inner join dbo.OrderItems  b on a.Id = b.OrderId
inner join dbo.Books on b.BookId = Books.Id
where a.Id not in (	
					select  distinct OrderId
					from dbo.OrderItems
					where BookId = 	(
										select top 1  BookId 
										from dbo.OrderItems
										group by BookId
										order by sum(Quantity) desc
									)
				
				)
				;

GO


