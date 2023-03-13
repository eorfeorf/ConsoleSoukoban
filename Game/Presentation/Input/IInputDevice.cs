using System;

namespace Souko.Game.Presentation.Input
{
    /// <summary>
    /// 入力を行うデバイスを吸収するインターフェイス.
    /// </summary>
    public interface IInputDevice
    {
        /// <summary>
        /// キー取得.
        /// TODO:どうやって吸収しよう
        /// </summary>
        /// <returns></returns>
        public ConsoleKey GetKey();
    }
}