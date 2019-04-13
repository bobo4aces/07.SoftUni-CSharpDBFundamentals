namespace FastFood.Web.MappingConfiguration
{
    using AutoMapper;
    using FastFood.Web.ViewModels.Categories;
    using FastFood.Web.ViewModels.Employees;
    using FastFood.Web.ViewModels.Items;
    using FastFood.Web.ViewModels.Orders;
    using Models;

    using ViewModels.Positions;

    public class FastFoodProfile : Profile
    {
        public FastFoodProfile()
        {
            //Positions
            this.CreateMap<CreatePositionInputModel, Position>()
                .ForMember(x => x.Name, y => y.MapFrom(s => s.PositionName));

            this.CreateMap<Position, PositionsAllViewModel>()
                .ForMember(x => x.Name, y => y.MapFrom(s => s.Name));

            //Orders
            this.CreateMap<CreateOrderInputModel, Order>()
                .ForMember(x => x.Customer, y => y.MapFrom(s => s.Customer))
                .ForMember(x => x.EmployeeId, y => y.MapFrom(s => s.EmployeeId));
            this.CreateMap<CreateOrderInputModel, OrderItem>()
                .ForMember(x => x.Quantity, y => y.MapFrom(s => s.Quantity))
                .ForMember(x => x.ItemId, y => y.MapFrom(s => s.ItemId));
            this.CreateMap<Order, OrderAllViewModel>()
                .ForMember(x => x.Customer, y => y.MapFrom(s => s.Customer))
                .ForMember(x => x.DateTime, y => y.MapFrom(s => s.DateTime))
                .ForMember(x => x.Employee, y => y.MapFrom(s => s.Employee))
                .ForMember(x => x.OrderId, y => y.MapFrom(s => s.Id));
            this.CreateMap<OrderItem, OrderAllViewModel>()
                .ForMember(x => x.OrderId, y => y.MapFrom(s => s.OrderId));

            //Items
            this.CreateMap<CreateItemInputModel, Item>()
                .ForMember(x => x.CategoryId, y => y.MapFrom(s => s.CategoryId))
                .ForMember(x => x.Name, y => y.MapFrom(s => s.Name))
                .ForMember(x => x.Price, y => y.MapFrom(s => s.Price));
            this.CreateMap<CreateItemViewModel, Item>()
                .ForMember(x => x.CategoryId, y => y.MapFrom(s => s.CategoryId));
            this.CreateMap<CreateItemViewModel, Category>()
                .ForMember(x => x.Id, y => y.MapFrom(s => s.CategoryId));
            this.CreateMap<CreateItemViewModel, CreateItemInputModel>()
                .ForMember(x => x.CategoryId, y => y.MapFrom(s => s.CategoryId));
            this.CreateMap<CreateItemInputModel, CreateItemViewModel>()
                .ForMember(x => x.CategoryId, y => y.MapFrom(s => s.CategoryId));
            this.CreateMap<Item, ItemsAllViewModels>()
                .ForMember(x => x.Category, y => y.MapFrom(s => s.Category))
                .ForMember(x => x.Name, y => y.MapFrom(s => s.Name))
                .ForMember(x => x.Price, y => y.MapFrom(s => s.Price));

            //Employees
            this.CreateMap<RegisterEmployeeInputModel, Employee>()
                .ForMember(x => x.Address, y => y.MapFrom(s => s.Address))
                .ForMember(x => x.Age, y => y.MapFrom(s => s.Age))
                .ForMember(x => x.Name, y => y.MapFrom(s => s.Name))
                .ForMember(x => x.PositionId, y => y.MapFrom(s => s.PositionId));
                //.ForMember(x => x.Position.Name, y => y.MapFrom(s => s.PositionName));
            this.CreateMap<Employee, RegisterEmployeeViewModel>()
                .ForMember(x => x.PositionId, y => y.MapFrom(s => s.PositionId));
            this.CreateMap<Employee, EmployeesAllViewModel>()
                .ForMember(x => x.Address, y => y.MapFrom(s => s.Address))
                .ForMember(x => x.Age, y => y.MapFrom(s => s.Age))
                .ForMember(x => x.Name, y => y.MapFrom(s => s.Name))
                .ForMember(x => x.Position, y => y.MapFrom(s => s.Position));

            //Categories
            this.CreateMap<CreateCategoryInputModel, Category>()
                .ForMember(x => x.Name, y => y.MapFrom(s => s.CategoryName));
            this.CreateMap<Category, CategoryAllViewModel>()
                .ForMember(x => x.Name, y => y.MapFrom(s => s.Name));
        }
    }
}
