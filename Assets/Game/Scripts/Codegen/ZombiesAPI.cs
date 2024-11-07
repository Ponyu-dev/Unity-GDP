/**
* Code generation. Don't modify! 
**/

using UnityEngine;
using Atomic.Contexts;
using System.Runtime.CompilerServices;
using Atomic.Entities;
using Atomic.Elements;
using System.Collections.Generic;
using Game.Scripts.Contexts.ZombieContext;

namespace Atomic.Contexts
{
	public static class ZombiesAPI
	{
		///Keys
		public const int AttackPlayer = 7; // Const<IEntity>
		public const int ZombieSystemData = 8; // ZombieSystemData
		public const int ZombieActiveMax = 10; // Const<int>


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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static ZombieSystemData GetZombieSystemData(this IContext obj) => obj.ResolveValue<ZombieSystemData>(ZombieSystemData);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetZombieSystemData(this IContext obj, out ZombieSystemData value) => obj.TryResolveValue(ZombieSystemData, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddZombieSystemData(this IContext obj, ZombieSystemData value) => obj.AddValue(ZombieSystemData, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelZombieSystemData(this IContext obj) => obj.DelValue(ZombieSystemData);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetZombieSystemData(this IContext obj, ZombieSystemData value) => obj.SetValue(ZombieSystemData, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasZombieSystemData(this IContext obj) => obj.HasValue(ZombieSystemData);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Const<int> GetZombieActiveMax(this IContext obj) => obj.ResolveValue<Const<int>>(ZombieActiveMax);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetZombieActiveMax(this IContext obj, out Const<int> value) => obj.TryResolveValue(ZombieActiveMax, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddZombieActiveMax(this IContext obj, Const<int> value) => obj.AddValue(ZombieActiveMax, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelZombieActiveMax(this IContext obj) => obj.DelValue(ZombieActiveMax);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetZombieActiveMax(this IContext obj, Const<int> value) => obj.SetValue(ZombieActiveMax, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasZombieActiveMax(this IContext obj) => obj.HasValue(ZombieActiveMax);
    }
}
