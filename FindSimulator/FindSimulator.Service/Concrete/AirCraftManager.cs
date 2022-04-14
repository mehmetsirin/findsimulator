using AutoMapper;

using FindSimulator.Domain.Entities;
using FindSimulator.Service.Abstract;
using FindSimulator.Service.Model.AirCraft;
using FindSimulator.Share.ComplexTypes;
using FindSimulator.Share.Results.Concrete;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FindSimulator.Service.Concrete
{
    public class AirCraftManager : IAirCraftManager
    {

        public readonly IBaseManager<int> baseManager;
        public readonly IMapper mapper;
        public AirCraftManager(IBaseManager<int> baseManager,IMapper mapper)
        {
            this.baseManager = baseManager;
            this.mapper = mapper;
        }

        public  async Task<DataResult<List<AirCraftView>>> List()
        {

            var crafts = await baseManager.ListAsync<AirCraft>();
            var craftsMapper = mapper.Map<List<AirCraftView>>(crafts.Data);
            return new DataResult<List<AirCraftView>>(ResultStatus.Success,craftsMapper);
        }

        public async Task<object> ListGroupAsync()
        {
           var  airCraftGroupViews = new List<AirCraftGroupView>();

            var crafts =  await baseManager.ListAsync<AirCraft>();

            var groupDatas = crafts.Data.GroupBy(y => y.Name).ToList() ;
            foreach (var   group in groupDatas)
            {
                var craft = new AirCraftGroupView();
                craft.Name = group.Key;
                craft.Locations = new List<LocationCraft>();
                foreach ( AirCraft item in   group)
                {
                     var location = new LocationCraft(item.District,item.Province,item.ID);
                    craft.Locations.Add(location);
                }
                airCraftGroupViews.Add(craft);
              
            }

            return airCraftGroupViews;
        }
    }
}
