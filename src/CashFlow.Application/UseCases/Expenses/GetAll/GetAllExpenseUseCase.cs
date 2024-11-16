using AutoMapper;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Repositories.Expenses;

namespace CashFlow.Application.UseCases.Expenses.GetAll;
public class GetAllExpenseUseCase : IGetAllExpenseUseCase
{
    private readonly IExpensesReadOnlyRepository _repositoy;
    private readonly IMapper _mapper;
    public GetAllExpenseUseCase(IExpensesReadOnlyRepository repository, IMapper mapper)
    {
        _repositoy = repository;
        _mapper = mapper;
    }
    public async Task<ResponseExpensesJson> Execute()
    {
        var result = await _repositoy.GetAll();

        return new ResponseExpensesJson
        {
            Expenses = _mapper.Map<List<ResponseShortExpenseJson>>(result)
        };
    }
}
