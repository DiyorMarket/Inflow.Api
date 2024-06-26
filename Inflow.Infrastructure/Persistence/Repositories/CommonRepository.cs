﻿using Inflow.Domain.Interfaces.Repositories;

namespace Inflow.Infrastructure.Persistence.Repositories
{
    public class CommonRepository : ICommonRepository
    {
        private readonly InflowDbContext _context;

        private ICategoryRepository _category;
        public ICategoryRepository Category
        {
            get
            {
                _category ??= new CategoryRepository(_context);

                return _category;
            }
        }

        private IProductRepository _product;
        public IProductRepository Product
        {
            get
            {
                _product ??= new ProductRepository(_context);

                return _product;
            }
        }

        private ICustomerRepository _customer;
        public ICustomerRepository Customer
        {
            get
            {
                _customer ??= new CustomerRepository(_context);

                return _customer;
            }
        }

        private ISaleRepository _sale;
        public ISaleRepository Sale
        {
            get
            {
                _sale ??= new SaleRepository(_context);

                return _sale;
            }
        }

        private ISaleItemRepository _saleItem;
        public ISaleItemRepository SaleItem
        {
            get
            {
                _saleItem ??= new SaleItemRepository(_context);

                return _saleItem;
            }
        }

        private ISupplierRepository _supplier;
        public ISupplierRepository Supplier
        {
            get
            {
                _supplier ??= new SupplierRepository(_context);

                return _supplier;
            }
        }

        private ISupplyRepository _supply;
        public ISupplyRepository Supply
        {
            get
            {
                _supply ??= new SupplyRepository(_context);

                return _supply;
            }
        }

        private ISupplyItemRepository _supplyItem;
        public ISupplyItemRepository SupplyItem
        {
            get
            {
                _supplyItem ??= new SupplyItemRepository(_context);

                return _supplyItem;
            }
        }
        public CommonRepository(InflowDbContext context)
        {
            _context = context;

            _category = new CategoryRepository(context);
            _product = new ProductRepository(context);
            _customer = new CustomerRepository(context);
            _sale = new SaleRepository(context);
            _saleItem = new SaleItemRepository(context);
            _supplier = new SupplierRepository(context);
            _supply = new SupplyRepository(context);
            _supplyItem = new SupplyItemRepository(context);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }
    }
}
