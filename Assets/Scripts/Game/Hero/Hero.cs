using System.Collections.Generic;
using Game.Item;

namespace Game.Hero
{
    public class Hero
    {
        private List<HeroSlot> _heroSlots  = new List<HeroSlot>();
        private List<string> _heroParties = new List<string>();
        public int Count => _heroSlots.Count;

        public void Load(List<HeroSlot> slots, List<string> parties)
        {
            _heroSlots = new List<HeroSlot>(slots);
            _heroParties = new List<string>(parties);
        }
        
        public void AddHero(HeroData data)
        {
            _heroSlots.Add(new HeroSlot(data));
            if (_heroParties.Count < 3)
            {
                _heroParties.Add(data.id);
            }
        }

        public List<HeroSlot> GetHeroSlots()
        {
            return _heroSlots;
        }
        
        public List<HeroSlot> GetHeroParties()
        {
            return _heroSlots.FindAll(x => _heroParties.Contains(x.heroData.id));
        }
        
        public List<string> GetHeroPartiesId()
        {
            return _heroParties;
        }
        
        public HeroSlot GetHero(int index)
        {
            return _heroSlots[index];
        }
        
        public HeroSlot GetHero(string id)
        {
            return _heroSlots.Find(x => x.heroData.id == id);
        }
        
        public HeroSlot GetHero(HeroData data)
        {
            return _heroSlots.Find(x => x.heroData.id == data.id);
        }

        private void UpdateParties()
        {
            
        }
    }
}