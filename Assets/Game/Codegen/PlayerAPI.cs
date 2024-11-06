/**
* Code generation. Don't modify! 
**/

using UnityEngine;
using Atomic.Contexts;
using System.Runtime.CompilerServices;
using Atomic.Entities;
using Atomic.Elements;
using System.Collections.Generic;

namespace Atomic.Contexts
{
	public static class PlayerAPI
	{
		///Keys
		public const int Character = 1; // Const<IEntity>


		///Extensions
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Const<IEntity> GetCharacter(this IContext obj) => obj.ResolveValue<Const<IEntity>>(Character);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetCharacter(this IContext obj, out Const<IEntity> value) => obj.TryResolveValue(Character, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddCharacter(this IContext obj, Const<IEntity> value) => obj.AddValue(Character, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelCharacter(this IContext obj) => obj.DelValue(Character);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetCharacter(this IContext obj, Const<IEntity> value) => obj.SetValue(Character, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasCharacter(this IContext obj) => obj.HasValue(Character);
    }
}
