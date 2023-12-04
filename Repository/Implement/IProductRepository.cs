using Dapper;
using DapperApp.Models;

namespace DapperApp.Repository.Implement
{
    public interface IProductRepository
    {
        Task CreateProduct(Products products);
        Task UpdatePoduct(Products poduct);
        Task<IEnumerable<Products>> GetAllProducts();
        Task<Products> GetProductsById(int id);
        Task CrreateMeasureUnit(Units units);
        Task UpdateMeasureUnit(Units units);
        Task DeleteMeasureUnit(int id);
        Task<IEnumerable<Units>> GetAllUnits();
        Task<Units> GetUnitsById(int id);
        Task CreateCategory(Categories category);
        Task UpdateCategory(Categories category);
        Task DeleteCategory(int id);
        Task<IEnumerable<Categories>> GetAllCategories();
        Task<Categories> GetCategoriesById(int id);
    }
    public class ProductRepository : IProductRepository
    {
        private readonly DapperContextClass context;
        public ProductRepository(DapperContextClass context)
        {
            this.context=context;
        }

        public async Task CreateCategory(Categories category)
        {
            string query = "INSERT INTO \"Categories\"(\"CategoryName\") VALUES (@CategoryName); ";
            using (var cnn = context.CeateConnection())
            {
                await cnn.ExecuteAsync(query, category);
            }
        }

        public async Task CreateProduct(Products products)
        {
            using (var cnn = context.CeateConnection())
            {
                cnn.Open();
                using (var trans = cnn.BeginTransaction())
                {
                    try
                    {
                        string query = "INSERT INTO public.\"Products\"(\"ProName\", \"ProCode\", \"ProDescription\", \"Qty\", \"BuyingPrice\", \"SellingPrice\", \"ReorderLevel\", \"CategoryId\", \"UnitId\") VALUES (@proName, @proCode, @proDescription, @qty, @buyingPrice, @sellingPrice, @reorderLevel, @categoryId, @unitId); ";
                        await cnn.ExecuteAsync(query, products);
                        trans.Commit();
                    }
                    catch (Exception e)
                    {
                        trans.Rollback();
                    }
                }
                cnn.Close();
            }
        }

        public async Task CrreateMeasureUnit(Units units)
        {
            using (var cnn = context.CeateConnection())
            {
                string query = "INSERT INTO \"Units\"(\"UnitName\") VALUES (@UnitName); ";
                await cnn.ExecuteAsync(query, units);
            }
        }

        public async Task DeleteCategory(int id)
        {
            using (var cnn = context.CeateConnection())
            {
                string query = "DELETE FROM \"Categories\" WHERE \"CategoryId\" = @id ; ";
                await cnn.ExecuteAsync(query, new { id });
            }
        }

        public async Task DeleteMeasureUnit(int id)
        {
            using (var cnn = context.CeateConnection())
            {
                var query = "DELETE FROM \"Units\" WHERE \"UnitId\" = @id ; ";
                await cnn.ExecuteAsync(query, new { id });
            }
        }

        public async Task<IEnumerable<Categories>> GetAllCategories()
        {
            using (var cnn = context.CeateConnection())
            {
                var query = "SELECT * FROM \"Categories\" ORDER BY \"CategoryId\" ASC  ";
                var detail = await cnn.QueryAsync<Categories>(query);
                return detail.ToList();
            }
        }

        public async Task<IEnumerable<Products>> GetAllProducts()
        {
            using (var cnn = context.CeateConnection())
            {
                var query = "SELECT a.*, b.\"CategoryName\", c.\"UnitName\"   " +
                    "FROM \"Products\" a left join \"Categories\" b on b.\"CategoryId\" = a.\"CategoryId\" left join \"Units\" c on c.\"UnitId\" = a.\"UnitId\" ORDER BY a.\"ProductId\" ASC;   ";
                var product = await cnn.QueryAsync<Products>(query);
                return product.ToList();
            }
        }

        public async Task<IEnumerable<Units>> GetAllUnits()
        {
            using (var cnn = context.CeateConnection())
            {
                var query = "SELECT * FROM \"Units\" ORDER BY \"UnitId\" ASC  ";
                var units = await cnn.QueryAsync<Units>(query);
                return units.ToList();
            }
        }

        public async Task<Categories> GetCategoriesById(int id)
        {
            using (var cnn = context.CeateConnection())
            {
                var query = "SELECT * FROM public.\"Categories\" WHERE \"CategoryId\" = @id; ";
                var result = await cnn.QueryFirstOrDefaultAsync<Categories>(query, new { id });
                return result;
            }
        }

        public async Task<Products> GetProductsById(int id)
        {
            using (var cnn = context.CeateConnection())
            {
                var query = "SELECT a.*, b.\"CategoryName\", c.\"UnitName\"     " +
                    "FROM \"Products\" a left join \"Categories\" b on b.\"CategoryId\" = a.\"CategoryId\"                   " +
                    "left join \"Units\" c on c.\"UnitId\" = a.\"UnitId\" WHERE a.\"ProductId\" = @id ;";
                var result = await cnn.QueryFirstOrDefaultAsync<Products>(query, new { id });
                return result;
            }
        }

        public async Task<Units> GetUnitsById(int id)
        {
            using (var cnn = context.CeateConnection())
            {
                var query = "SELECT * FROM \"Units\" where \"UnitId\" = @id; ";
                var result = await cnn.QueryFirstOrDefaultAsync<Units>(query, new { id });
                return result;
            }
        }

        public async Task UpdateCategory(Categories category)
        {
            using (var cnn = context.CeateConnection())
            {
                var query = "UPDATE \"Categories\" SET \"CategoryName\"= '"+ category.CategoryName +"' WHERE \"CategoryId\"= '"+ category.CategoryId +"'; ";
                await cnn.ExecuteAsync(query, category);
            }
        }

        public async Task UpdateMeasureUnit(Units units)
        {
            using (var cnn = context.CeateConnection())
            {
                var query = "UPDATE \"Units\" SET \"UnitName\" = '"+units.UnitName+"' WHERE \"UnitId\" = '"+units.UnitId+"'; ";
                await cnn.ExecuteAsync(query, units);
            }
        }

        public async Task UpdatePoduct(Products poduct)
        {
            using (var cnn = context.CeateConnection())
            {
                var query = "UPDATE \"Products\" SET \"ProName\"='"+poduct.ProName+"', \"ProCode\"='"+poduct.ProCode+"', \"ProDescription\"='"+poduct.ProDescription+"',  " +
                    " \"Qty\"='"+poduct.Qty+"', \"BuyingPrice\"='"+poduct.BuyingPrice+"', \"SellingPrice\"='"+poduct.SellingPrice+"', \"ReorderLevel\"='"+poduct.ReorderLevel+"',  " +
                    " \"CategoryId\"='"+poduct.CategoryId+"', \"UnitId\"='"+poduct.UnitId+"' WHERE \"ProductId\"= '"+poduct.ProductId+"'; ";
                await cnn.ExecuteAsync(query, poduct);
            }
        }
    }
}
