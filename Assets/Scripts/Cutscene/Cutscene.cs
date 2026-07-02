using System.Collections.Generic;

namespace Cutscene
{
    public class Cutscene
    {
        private List<string> _cutscenes = new List<string>();
        
        public int CutSceneCount => _cutscenes.Count;

        public void Load(List<string> cutscenes)
        {
            _cutscenes = new List<string>(cutscenes);
        }
        
        public void Add(string id)
        {
            _cutscenes.Add(id);
        }
        public bool Check(string id)
        {
            return CutSceneCount != 0 && _cutscenes.Contains(id);
        }
        
        public List<string> GetCutscenes()
        {
            return _cutscenes;
        }
    }
}