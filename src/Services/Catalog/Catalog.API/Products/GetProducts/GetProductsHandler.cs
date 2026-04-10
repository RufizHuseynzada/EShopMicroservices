
using Marten.Pagination;

namespace Catalog.API.Products.GetProducts
{
    public record GetProductsQyuery(int? PageNumber = 1, int? PageSize = 10) : IQuery<GetProductsResult>;
    public record GetProductsResult(IEnumerable<Product> Products);
    internal class GetProductsQueryHandler(IDocumentSession session, ILogger<GetProductsQueryHandler> logger) : IQueryHandler<GetProductsQyuery, GetProductsResult>
    {
        public async Task<GetProductsResult> Handle(GetProductsQyuery query, CancellationToken cancellationToken)
        { 
            var products = await session.Query<Product>().ToPagedListAsync(query.PageNumber ?? 1, query.PageSize ?? 10,cancellationToken);

            return new GetProductsResult(products);
        }
    }
}
