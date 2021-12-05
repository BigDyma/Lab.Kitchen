using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kitchen.Models
{
    public class Order : BaseEntity
    {
        public List<Guid> Items { get; set; }
        public int Priority { get; set; }
        public float MaxWaitTime { get; set; }
        public Guid TableId { get; set; }

        public DateTime ReceivedAt { get; set; }

        public List<KitchenFood> RealItems { get; set; }

        public bool IsReady
        {
            get
            {
                bool ready = true;
                RealItems.ForEach(x =>
                {
                    ready = x.State == KitchenFoodState.Ready;
                });
                return ready;
            }
            
        }


    }
}
