using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Producer
{
    public interface IMessageProducer
    {
        Task SendMessage<T> (T message);
    }
}
