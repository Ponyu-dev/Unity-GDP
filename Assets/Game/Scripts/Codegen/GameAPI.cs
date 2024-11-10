/**
* Code generation. Don't modify! 
**/

using UnityEngine;
using Atomic.Contexts;
using System.Runtime.CompilerServices;
using Atomic.Entities;
using Atomic.Elements;
using System.Collections.Generic;
using Game.Scripts.Common.Team;

namespace Atomic.Contexts
{
	public static class GameAPI
	{
		///Keys
		public const int PlayerMap = 3; // Dictionary<TeamType, IContext>
		public const int WorldTransform = 5; // Transform


		///Extensions
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Dictionary<TeamType, IContext> GetPlayerMap(this IContext obj) => obj.ResolveValue<Dictionary<TeamType, IContext>>(PlayerMap);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetPlayerMap(this IContext obj, out Dictionary<TeamType, IContext> value) => obj.TryResolveValue(PlayerMap, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddPlayerMap(this IContext obj, Dictionary<TeamType, IContext> value) => obj.AddValue(PlayerMap, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelPlayerMap(this IContext obj) => obj.DelValue(PlayerMap);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetPlayerMap(this IContext obj, Dictionary<TeamType, IContext> value) => obj.SetValue(PlayerMap, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasPlayerMap(this IContext obj) => obj.HasValue(PlayerMap);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Transform GetWorldTransform(this IContext obj) => obj.ResolveValue<Transform>(WorldTransform);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetWorldTransform(this IContext obj, out Transform value) => obj.TryResolveValue(WorldTransform, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddWorldTransform(this IContext obj, Transform value) => obj.AddValue(WorldTransform, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelWorldTransform(this IContext obj) => obj.DelValue(WorldTransform);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetWorldTransform(this IContext obj, Transform value) => obj.SetValue(WorldTransform, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasWorldTransform(this IContext obj) => obj.HasValue(WorldTransform);
    }
}
