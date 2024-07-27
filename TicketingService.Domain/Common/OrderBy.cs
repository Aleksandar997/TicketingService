using System.Linq.Expressions;

namespace TicketingService.Domain.Common
{
    public class OrderBy<Ttype>
    {
        private Expression<Func<Ttype, object>> _expression;

        public OrderBy(Expression<Func<Ttype, object>> expression)
        {
            _expression = expression;
        }

        public Expression<Func<Ttype, object>> Expression => _expression;
    }
}
