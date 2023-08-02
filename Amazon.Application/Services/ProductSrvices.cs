using Amazon.Application.Contracts;
using Amazon.Domain;
using Amazon.DTO;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Amazon.Application.Services
{
    public class ProductSrvices : IProductServices
    {
        private readonly IProductReposatory _reposatory;
        private readonly IMapper _mapper;
        private readonly IImageReposatory _Imgrepo;
        public ProductSrvices(IProductReposatory reposatory, IMapper mapper,IImageReposatory imageReposatory)
        {
            _reposatory = reposatory;
            _mapper = mapper;
            _Imgrepo= imageReposatory;
        }

        public async Task<List<ShowProductDTO>> FilterByPrice(int catid,decimal initprice, decimal finalprice)
        {
            var res = await _reposatory.GetAllAsync();
            var FillterdList = res.Where(p => p.Price >= initprice && p.Price <= finalprice && p.CategoryId==catid&&p.Status==true);
            List<ShowProductDTO> Products = _mapper.Map<List<ShowProductDTO>>(FillterdList);
            foreach(var Product in Products)
            {
                Product.images = await _Imgrepo.GetImagesByPrdId(Product.Id);
            }
            return Products;
        }

        public async Task<List<ShowProductDTO>> GetAllProducts()
        {
            var products = await _reposatory.GetAllAsync();
            return _mapper.Map<List<ShowProductDTO>>(products);
        }

        public async Task<List<ShowProductDTO>> GetProductsByCategoryId(int categoryId)
        {
            var products = await _reposatory.GetAllAsync();
            var filtterdproducts = products.
                Where(p => p.CategoryId == categoryId && p.UnitInStock > 0 && p.Status == true);
            var ProductsList= _mapper.Map<List<ShowProductDTO>>(filtterdproducts);
            foreach (var item in ProductsList)
            {
                item.images = await _Imgrepo.GetImagesByPrdId(item.Id);
            }
            return ProductsList;
        }

        public async Task<List<ShowProductDTO>> SearchByProductName(string name)
        {
            var res = await _reposatory.GetAllAsync();
            var FillterdList=res.Where(p=>p.Name.ToLower().Contains(name.ToLower()));
            return _mapper.Map<List<ShowProductDTO>>(FillterdList);
        }
        #region User Services

        public async Task<List<ShowProductDTO>> ShowProductsPagination(int pagenumber, int items)
        {
            var products = await _reposatory.GetAllAsync();
            var paginatedList = products.Where(p => p.UnitInStock > 0 && p.Status == true)
                .Skip(items * (pagenumber - 1)).Take(items);
            return _mapper.Map<List<ShowProductDTO>>(paginatedList);
        }

        public async Task<List<ShowProductDTO>> SearchByArProductName(string Arname)
        {
            var res = await _reposatory.GetAllAsync();
            var FillterdList = res.Where(p => p.arabicName.Contains(Arname));
            return _mapper.Map<List<ShowProductDTO>>(FillterdList);
        }

        public async Task<ShowProductDTO> GetProductsById(int id)
        {
            var res = await _reposatory.GetByIdAsync(id);
            var product = _mapper.Map<ShowProductDTO>(res);
            product.images = await _Imgrepo.GetImagesByPrdId(id);
            return product;
        }

        public async Task<PriceDTO> GetPriceCategoryId(int id)
        {
            var products = await _reposatory.GetAllAsync();
            var filtterdproducts = products.
                Where(p => p.CategoryId == id && p.Status == true).ToList();
            decimal minprice = 0;
            decimal maxprice = 100;
            if (filtterdproducts.Count > 0)
            {
                minprice = filtterdproducts.Min(p => p.Price);
                maxprice = filtterdproducts.Max(p => p.Price);
            }

            return new PriceDTO() { MaxPrice = maxprice, MinPrice = minprice };
        }

        public async Task<List<ShowProductDTO>> GetProductsWithMaxPriceFillter(int catid, decimal max)
        {
            var res = await _reposatory.GetAllAsync();
            var FillterdList = res.Where(p => p.Price >= max && p.CategoryId == catid && p.Status == true);
            List<ShowProductDTO> Products = _mapper.Map<List<ShowProductDTO>>(FillterdList);
            foreach (var Product in Products)
            {
                Product.images = await _Imgrepo.GetImagesByPrdId(Product.Id);
            }
            return Products;
        } 
        #endregion


        public async Task<ShowProductDTO> CreateProduct(AddUpdateProductDTO product)
		{
			var res=await _reposatory.CreateAsync(_mapper.Map<Product>(product));
            if (res !=null)
            {
                await _reposatory.SaveChangesAsync();
                return _mapper.Map<ShowProductDTO>(res);
            }
            else
            {
                return null;
            }
		}

		public async Task<bool> UpdateProduct(int id, AddUpdateProductDTO product)
		{
            Product product1 = _mapper.Map<Product>(product);
            product1.Id = id;
            var res = await _reposatory.UpdateAsync(product1, id);
            if (res)
            {
                await _reposatory.SaveChangesAsync();
                return true;
            }
            else { return false; }
		}

		public async Task<bool> DeleteProduct(int id)
		{
            var res = await _reposatory.DeleteAsync(id);
            if (res)
            {
                await _reposatory.SaveChangesAsync();
                return true;
            }
            else
            {
                return false;
            }
		}
        public async Task<bool> IncreamnteUnitInStock(int id, int count)
        {
            var product=await _reposatory.GetByIdAsync(id);
            product.UnitInStock += count;
            var res=await _reposatory.UpdateAsync(product, id);
            await _reposatory.SaveChangesAsync();
            return res;
        }
        public async Task<bool> decreamnteUnitInStock(int id,int count)
        {
            var product = await _reposatory.GetByIdAsync(id);
            product.UnitInStock -= count;
            var res = await _reposatory.UpdateAsync(product, id);
            await _reposatory.SaveChangesAsync();
            return res;
        }
    }
}
