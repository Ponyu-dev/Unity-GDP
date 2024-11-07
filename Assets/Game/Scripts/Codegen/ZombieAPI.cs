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
	public static class ZombieAPI
	{
		///Keys
		public const int AttackPlayer = 7; // Const<IEntity>


		///Extensions
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Const<IEntity> GetAttackPlayer(this IContext obj) => obj.ResolveValue<Const<IEntity>>(AttackPlayer);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetAttackPlayer(this IContext obj, out Const<IEntity> value) => obj.TryResolveValue(AttackPlayer, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddAttackPlayer(this IContext obj, Const<IEntity> value) => obj.AddValue(AttackPlayer, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelAttackPlayer(this IContext obj) => obj.DelValue(AttackPlayer);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetAttackPlayer(this IContext obj, Const<IEntity> value) => obj.SetValue(AttackPlayer, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasAttackPlayer(this IContext obj) => obj.HasValue(AttackPlayer);
    }
}
