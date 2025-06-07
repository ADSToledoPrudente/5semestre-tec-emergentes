namespace backend_fila.Data.DTO.Customer
{
    public class CustomerDTO
    {
        public int? Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool? Priority { get; set; } = false;
        public DateTime Date { get; set; }
        public bool? Active { get; set; }
    }
}
