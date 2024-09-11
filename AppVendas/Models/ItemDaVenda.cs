namespace AppVendas.Models
{
    public class ItemDaVenda
    {
        public Guid ItemDavendaId { get; set; }
        /*relação com a venda*/
        public Guid VendaId { get; set; }
        public Venda? Venda { get; set; }
        /* relação com produto*/
        public Guid ProdutoId { get; set; }
        public Produto? Produto { get; set; }
        public double QtdadeVendida { get; set; }
        public double ValorTotalDoItem { get; set; }

    }
}
