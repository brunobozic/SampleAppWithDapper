CREATE TABLE #Products
(
	Id int, 
	Name NVarchar(128),
	Price decimal,
	CategoryName NVarchar(128),
	PictureUrl NVarchar(max),
	ManufacturerName NVarchar(128),
	ManufacturerMainPictureUrl NVarchar(max),
	OrdersNumber int,
	AverageReviewRating float
)