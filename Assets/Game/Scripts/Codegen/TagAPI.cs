/**
* Code generation. Don't modify! 
**/

using UnityEngine;
using Atomic.Entities;

namespace Atomic.Entities
{
    public static class TagAPI
    {
        ///Keys
        public const int Character = 1;
        public const int Zombie = 2;


        ///Extensions
        public static bool HasCharacterTag(this IEntity obj) => obj.HasTag(Character);
        public static bool NotCharacterTag(this IEntity obj) => !obj.HasTag(Character);
        public static bool AddCharacterTag(this IEntity obj) => obj.AddTag(Character);
        public static bool DelCharacterTag(this IEntity obj) => obj.DelTag(Character);

        public static bool HasZombieTag(this IEntity obj) => obj.HasTag(Zombie);
        public static bool NotZombieTag(this IEntity obj) => !obj.HasTag(Zombie);
        public static bool AddZombieTag(this IEntity obj) => obj.AddTag(Zombie);
        public static bool DelZombieTag(this IEntity obj) => obj.DelTag(Zombie);
    }
}
