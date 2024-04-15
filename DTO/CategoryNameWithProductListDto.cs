namespace WebApi.DTO
{
    public class CategoryNameWithProductListDto
    {
        public string Name { get; set; }

        public List<PrductListDto> Catproducts { get; set; } = new List<PrductListDto>();
    }
}
