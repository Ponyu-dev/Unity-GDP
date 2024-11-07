/**
* Code generation. Don't modify! 
**/

using UnityEngine;
using Atomic.Contexts;
using System.Runtime.CompilerServices;
using Atomic.Entities;
using Atomic.Elements;
using System.Collections.Generic;
using Game.Scripts.Contexts.PlayerContext;
using Game.Scripts.Common.Team;

namespace Atomic.Contexts
{
	public static class PlayerAPI
	{
		///Keys
		public const int Character = 1; // Const<IEntity>
		public const int InputMap = 2; // InputMap
		public const int TeamType = 4; // Const<TeamType>
		public const int CameraData = 6; // CameraData


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

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static InputMap GetInputMap(this IContext obj) => obj.ResolveValue<InputMap>(InputMap);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetInputMap(this IContext obj, out InputMap value) => obj.TryResolveValue(InputMap, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddInputMap(this IContext obj, InputMap value) => obj.AddValue(InputMap, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelInputMap(this IContext obj) => obj.DelValue(InputMap);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetInputMap(this IContext obj, InputMap value) => obj.SetValue(InputMap, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasInputMap(this IContext obj) => obj.HasValue(InputMap);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static Const<TeamType> GetTeamType(this IContext obj) => obj.ResolveValue<Const<TeamType>>(TeamType);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetTeamType(this IContext obj, out Const<TeamType> value) => obj.TryResolveValue(TeamType, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddTeamType(this IContext obj, Const<TeamType> value) => obj.AddValue(TeamType, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelTeamType(this IContext obj) => obj.DelValue(TeamType);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetTeamType(this IContext obj, Const<TeamType> value) => obj.SetValue(TeamType, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasTeamType(this IContext obj) => obj.HasValue(TeamType);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static CameraData GetCameraData(this IContext obj) => obj.ResolveValue<CameraData>(CameraData);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool TryGetCameraData(this IContext obj, out CameraData value) => obj.TryResolveValue(CameraData, out value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool AddCameraData(this IContext obj, CameraData value) => obj.AddValue(CameraData, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool DelCameraData(this IContext obj) => obj.DelValue(CameraData);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static void SetCameraData(this IContext obj, CameraData value) => obj.SetValue(CameraData, value);

		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		public static bool HasCameraData(this IContext obj) => obj.HasValue(CameraData);
    }
}
