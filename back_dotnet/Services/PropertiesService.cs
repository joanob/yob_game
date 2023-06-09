using Domain;

namespace Services;

public class PropertiesService : IPropertiesService
{
    private readonly IGameDataService _gamedataService;
    private readonly IUserService _userService;
    private readonly IPropertiesRepository _propertiesRepository;

    public PropertiesService(IGameDataService gameDataService, IUserService userService, IPropertiesRepository propertiesRepository)
    {
        this._gamedataService = gameDataService;
        this._userService = userService;
        this._propertiesRepository = propertiesRepository;
    }

    public async Task<Property> BuyProperty(Property property)
    {
        try
        {
            var user = await _userService.GetUserById(property.UserId);

            var prodBuilding = _gamedataService.GetProductionBuildingById(property.ProductionBuildingId);

            if (prodBuilding.BuildCost > user.CompanyMoney)
            {
                throw new Exception("Not enough money");
            }

            user.CompanyMoney -= (uint)prodBuilding.BuildCost;

            await _userService.UpdateCompanyMoney(user.Id, (int)user.CompanyMoney);

            return _propertiesRepository.CreateProperty(property);
        }
        catch (System.Exception)
        {

            throw;
        }
    }

    public async Task<List<Property>> GetAllProperties(int userId)
    {
        return await _propertiesRepository.GetAllProperties(userId);
    }

    public async Task<List<Property>> GetPropertiesByProductionBuildingId(int userId, int productionBuildingId)
    {
        return await _propertiesRepository.GetPropertiesByProductionBuildingId(userId, productionBuildingId);
    }
}