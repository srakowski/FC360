﻿namespace FC360.Core
{
	using System.Collections;

	public struct CharBitmap
	{
		// private BitArray _data;
		private byte[] _data;

		public CharBitmap(byte[] data)
		{
			//_data = new BitArray(data);
			_data = data;
		}

		//public bool this[int x, int y] => _data[(y * 8) + x];
		public bool this[int x, int y] => (_data[y] & (0x80 >> x)) != 0;
	}

	public class SysFont
	{
		public const int CharWidth = 6;

		public static int CharHeight = 8;

		public CharBitmap this[char c] => new CharBitmap(_data[c]);

		/// <summary>
		/// TODO: make these CharBitmap values at construction?
		/// </summary>
		private readonly byte[][] _data = new byte[][]
		{
			new byte[] {0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00}, // .
			new byte[] {0x7E, 0x81, 0xA5, 0x81, 0xBD, 0x99, 0x81, 0x7E}, // .
			new byte[] {0x7E, 0xFF, 0xDB, 0xFF, 0xC3, 0xE7, 0xFF, 0x7E}, // .
			new byte[] {0x6C, 0xFE, 0xFE, 0xFE, 0x7C, 0x38, 0x10, 0x00}, // .
			new byte[] {0x10, 0x38, 0x7C, 0xFE, 0x7C, 0x38, 0x10, 0x00}, // .
			new byte[] {0x38, 0x7C, 0x38, 0xFE, 0xFE, 0x7C, 0x38, 0x7C}, // .
			new byte[] {0x10, 0x10, 0x38, 0x7C, 0xFE, 0x7C, 0x38, 0x7C}, // .
			new byte[] {0x00, 0x00, 0x18, 0x3C, 0x3C, 0x18, 0x00, 0x00}, // .
			new byte[] {0xFF, 0xFF, 0xE7, 0xC3, 0xC3, 0xE7, 0xFF, 0xFF}, // .
			new byte[] {0x00, 0x3C, 0x66, 0x42, 0x42, 0x66, 0x3C, 0x00}, // .
			new byte[] {0xFF, 0xC3, 0x99, 0xBD, 0xBD, 0x99, 0xC3, 0xFF}, // .
			new byte[] {0x0F, 0x07, 0x0F, 0x7D, 0xCC, 0xCC, 0xCC, 0x78}, // .
			new byte[] {0x3C, 0x66, 0x66, 0x66, 0x3C, 0x18, 0x7E, 0x18}, // .
			new byte[] {0x3F, 0x33, 0x3F, 0x30, 0x30, 0x70, 0xF0, 0xE0}, // .
			new byte[] {0x7F, 0x63, 0x7F, 0x63, 0x63, 0x67, 0xE6, 0xC0}, // .
			new byte[] {0x99, 0x5A, 0x3C, 0xE7, 0xE7, 0x3C, 0x5A, 0x99}, // .
			new byte[] {0x80, 0xE0, 0xF8, 0xFE, 0xF8, 0xE0, 0x80, 0x00}, // .
			new byte[] {0x02, 0x0E, 0x3E, 0xFE, 0x3E, 0x0E, 0x02, 0x00}, // .
			new byte[] {0x18, 0x3C, 0x7E, 0x18, 0x18, 0x7E, 0x3C, 0x18}, // .
			new byte[] {0x66, 0x66, 0x66, 0x66, 0x66, 0x00, 0x66, 0x00}, // .
			new byte[] {0x7F, 0xDB, 0xDB, 0x7B, 0x1B, 0x1B, 0x1B, 0x00}, // .
			new byte[] {0x3E, 0x63, 0x38, 0x6C, 0x6C, 0x38, 0xCC, 0x78}, // .
			new byte[] {0x00, 0x00, 0x00, 0x00, 0x7E, 0x7E, 0x7E, 0x00}, // .
			new byte[] {0x18, 0x3C, 0x7E, 0x18, 0x7E, 0x3C, 0x18, 0xFF}, // .
			new byte[] {0x18, 0x3C, 0x7E, 0x18, 0x18, 0x18, 0x18, 0x00}, // .
			new byte[] {0x18, 0x18, 0x18, 0x18, 0x7E, 0x3C, 0x18, 0x00}, // .
			new byte[] {0x00, 0x18, 0x0C, 0xFE, 0x0C, 0x18, 0x00, 0x00}, // .
			new byte[] {0x00, 0x30, 0x60, 0xFE, 0x60, 0x30, 0x00, 0x00}, // .
			new byte[] {0x00, 0x00, 0xC0, 0xC0, 0xC0, 0xFE, 0x00, 0x00}, // .
			new byte[] {0x00, 0x24, 0x66, 0xFF, 0x66, 0x24, 0x00, 0x00}, // .
			new byte[] {0x00, 0x18, 0x3C, 0x7E, 0xFF, 0xFF, 0x00, 0x00}, // .
			new byte[] {0x00, 0xFF, 0xFF, 0x7E, 0x3C, 0x18, 0x00, 0x00}, // .
			new byte[] {0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00}, //  
			new byte[] {0x40, 0x40, 0x40, 0x40, 0x40, 0x00, 0x40, 0x00}, // !
			new byte[] {0x90, 0x90, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00}, // "
			new byte[] {0x50, 0x50, 0xF8, 0x50, 0xF8, 0x50, 0x50, 0x00}, // #
			new byte[] {0x20, 0x78, 0xA0, 0x70, 0x28, 0xF0, 0x20, 0x00}, // $
			new byte[] {0xC8, 0xC8, 0x10, 0x20, 0x40, 0x98, 0x98, 0x00}, // %
			new byte[] {0x70, 0x88, 0x50, 0x20, 0x54, 0x88, 0x74, 0x00}, // &
			new byte[] {0x60, 0x20, 0x40, 0x00, 0x00, 0x00, 0x00, 0x00}, // '
			new byte[] {0x20, 0x40, 0x80, 0x80, 0x80, 0x40, 0x20, 0x00}, // (
			new byte[] {0x20, 0x10, 0x08, 0x08, 0x08, 0x10, 0x20, 0x00}, // )
			new byte[] {0x00, 0x20, 0xA8, 0x70, 0x70, 0xA8, 0x20, 0x00}, // *
			new byte[] {0x00, 0x00, 0x20, 0x20, 0xF8, 0x20, 0x20, 0x00}, // +
			new byte[] {0x00, 0x00, 0x00, 0x00, 0x00, 0x60, 0x20, 0x40}, // ,
			new byte[] {0x00, 0x00, 0x00, 0x00, 0xF8, 0x00, 0x00, 0x00}, // -
			new byte[] {0x00, 0x00, 0x00, 0x00, 0x00, 0x60, 0x60, 0x00}, // .
			new byte[] {0x08, 0x10, 0x10, 0x20, 0x20, 0x40, 0x80, 0x00}, // /
			new byte[] {0x70, 0x88, 0x98, 0xA8, 0xC8, 0x88, 0x70, 0x00}, // 0
			new byte[] {0x20, 0x60, 0x20, 0x20, 0x20, 0x20, 0x70, 0x00}, // 1
			new byte[] {0x70, 0x88, 0x08, 0x10, 0x20, 0x40, 0xF8, 0x00}, // 2
			new byte[] {0xF8, 0x10, 0x20, 0x10, 0x08, 0x88, 0x70, 0x00}, // 3
			new byte[] {0x10, 0x30, 0x50, 0x90, 0xF8, 0x10, 0x10, 0x00}, // 4
			new byte[] {0xF8, 0x80, 0x80, 0xF0, 0x08, 0x88, 0x70, 0x00}, // 5
			new byte[] {0x20, 0x40, 0x80, 0xF0, 0x88, 0x88, 0x70, 0x00}, // 6
			new byte[] {0xF8, 0x08, 0x10, 0x20, 0x40, 0x40, 0x40, 0x00}, // 7
			new byte[] {0x70, 0x88, 0x88, 0x70, 0x88, 0x88, 0x70, 0x00}, // 8
			new byte[] {0x70, 0x88, 0x88, 0x78, 0x08, 0x08, 0x70, 0x00}, // 9
			new byte[] {0x00, 0x60, 0x60, 0x00, 0x60, 0x60, 0x00, 0x00}, // :
			new byte[] {0x00, 0x00, 0x60, 0x60, 0x00, 0x60, 0x60, 0x20}, // ;
			new byte[] {0x10, 0x20, 0x40, 0x80, 0x40, 0x20, 0x10, 0x00}, // <
			new byte[] {0x00, 0x00, 0xF8, 0x00, 0xF8, 0x00, 0x00, 0x00}, // =
			new byte[] {0x80, 0x40, 0x20, 0x10, 0x20, 0x40, 0x80, 0x00}, // >
			new byte[] {0x78, 0x84, 0x04, 0x08, 0x10, 0x00, 0x10, 0x00}, // ?
			new byte[] {0x70, 0x88, 0x88, 0xA8, 0xB8, 0x80, 0x78, 0x00}, // @
			new byte[] {0x70, 0x88, 0x88, 0xF8, 0x88, 0x88, 0x88, 0x00}, // A
			new byte[] {0xF0, 0x88, 0x88, 0xF0, 0x88, 0x88, 0xF0, 0x00}, // B
			new byte[] {0x70, 0x88, 0x80, 0x80, 0x80, 0x88, 0x70, 0x00}, // C
			new byte[] {0xF0, 0x88, 0x88, 0x88, 0x88, 0x88, 0xF0, 0x00}, // D
			new byte[] {0xF8, 0x80, 0x80, 0xF0, 0x80, 0x80, 0xF8, 0x00}, // E
			new byte[] {0xF8, 0x80, 0x80, 0xE0, 0x80, 0x80, 0x80, 0x00}, // F
			new byte[] {0x70, 0x88, 0x80, 0x80, 0x98, 0x88, 0x78, 0x00}, // G
			new byte[] {0x88, 0x88, 0x88, 0xF8, 0x88, 0x88, 0x88, 0x00}, // H
			new byte[] {0x70, 0x20, 0x20, 0x20, 0x20, 0x20, 0x70, 0x00}, // I
			new byte[] {0x38, 0x10, 0x10, 0x10, 0x10, 0x90, 0x60, 0x00}, // J
			new byte[] {0x88, 0x90, 0xA0, 0xC0, 0xA0, 0x90, 0x88, 0x00}, // K
			new byte[] {0x80, 0x80, 0x80, 0x80, 0x80, 0x80, 0xF8, 0x00}, // L
			new byte[] {0x88, 0xD8, 0xA8, 0xA8, 0x88, 0x88, 0x88, 0x00}, // M
			new byte[] {0x88, 0x88, 0xC8, 0xA8, 0x98, 0x88, 0x88, 0x00}, // N
			new byte[] {0x70, 0x88, 0x88, 0x88, 0x88, 0x88, 0x70, 0x00}, // O
			new byte[] {0xF0, 0x88, 0x88, 0xF0, 0x80, 0x80, 0x80, 0x00}, // P
			new byte[] {0x70, 0x88, 0x88, 0x88, 0xA8, 0x90, 0x68, 0x00}, // Q
			new byte[] {0xF0, 0x88, 0x88, 0xF0, 0xA0, 0x90, 0x88, 0x00}, // R
			new byte[] {0x70, 0x88, 0x80, 0x70, 0x08, 0x88, 0x70, 0x00}, // S
			new byte[] {0xF8, 0x20, 0x20, 0x20, 0x20, 0x20, 0x20, 0x00}, // T
			new byte[] {0x88, 0x88, 0x88, 0x88, 0x88, 0x88, 0x70, 0x00}, // U
			new byte[] {0x88, 0x88, 0x88, 0x50, 0x50, 0x20, 0x20, 0x00}, // V
			new byte[] {0x88, 0x88, 0x88, 0x88, 0xA8, 0xA8, 0x70, 0x00}, // W
			new byte[] {0x88, 0x88, 0x50, 0x20, 0x50, 0x88, 0x88, 0x00}, // X
			new byte[] {0x88, 0x88, 0x88, 0x50, 0x20, 0x20, 0x20, 0x00}, // Y
			new byte[] {0xF8, 0x08, 0x10, 0x20, 0x40, 0x80, 0xF8, 0x00}, // Z
			new byte[] {0xE0, 0x80, 0x80, 0x80, 0x80, 0x80, 0xE0, 0x00}, // [
			new byte[] {0x80, 0x40, 0x40, 0x20, 0x10, 0x10, 0x08, 0x00}, // backslash
			new byte[] {0xE0, 0x20, 0x20, 0x20, 0x20, 0x20, 0xE0, 0x00}, // ]
			new byte[] {0x20, 0x50, 0x88, 0x00, 0x00, 0x00, 0x00, 0x00}, // ^
			new byte[] {0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xF8, 0x00}, // _
			new byte[] {0x40, 0x20, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00}, // `
			new byte[] {0x00, 0x00, 0x70, 0x08, 0x78, 0x88, 0x78, 0x00}, // a
			new byte[] {0x80, 0x80, 0xB0, 0xC8, 0x88, 0x88, 0xF0, 0x00}, // b
			new byte[] {0x00, 0x00, 0x70, 0x88, 0x80, 0x88, 0x70, 0x00}, // c
			new byte[] {0x08, 0x08, 0x68, 0x98, 0x88, 0x98, 0x68, 0x00}, // d
			new byte[] {0x00, 0x00, 0x70, 0x88, 0xF8, 0x80, 0x70, 0x00}, // e
			new byte[] {0x30, 0x48, 0x40, 0xE0, 0x40, 0x40, 0x40, 0x00}, // f
			new byte[] {0x00, 0x00, 0x34, 0x48, 0x48, 0x38, 0x08, 0x30}, // g
			new byte[] {0x80, 0x80, 0xB0, 0xC8, 0x88, 0x88, 0x88, 0x00}, // h
			new byte[] {0x20, 0x00, 0x60, 0x20, 0x20, 0x20, 0x70, 0x00}, // i
			new byte[] {0x10, 0x00, 0x30, 0x10, 0x10, 0x10, 0x90, 0x60}, // j
			new byte[] {0x80, 0x80, 0x88, 0x90, 0xA0, 0xD0, 0x88, 0x00}, // k
			new byte[] {0x60, 0x20, 0x20, 0x20, 0x20, 0x20, 0x70, 0x00}, // l
			new byte[] {0x00, 0x00, 0xD0, 0xA8, 0xA8, 0x88, 0x88, 0x00}, // m
			new byte[] {0x00, 0x00, 0xB0, 0xC8, 0x88, 0x88, 0x88, 0x00}, // n
			new byte[] {0x00, 0x00, 0x70, 0x88, 0x88, 0x88, 0x70, 0x00}, // o
			new byte[] {0x00, 0x00, 0xF0, 0x88, 0xF0, 0x80, 0x80, 0x00}, // p
			new byte[] {0x00, 0x00, 0x68, 0x98, 0x98, 0x68, 0x08, 0x08}, // q
			new byte[] {0x00, 0x00, 0xB0, 0xC8, 0x80, 0x80, 0x80, 0x00}, // r
			new byte[] {0x00, 0x00, 0x78, 0x80, 0x70, 0x08, 0xF0, 0x00}, // s
			new byte[] {0x40, 0x40, 0xE0, 0x40, 0x40, 0x50, 0x20, 0x00}, // t
			new byte[] {0x00, 0x00, 0x88, 0x88, 0x88, 0x98, 0x68, 0x00}, // u
			new byte[] {0x00, 0x00, 0x88, 0x88, 0x88, 0x50, 0x20, 0x00}, // v
			new byte[] {0x00, 0x00, 0x88, 0x88, 0xA8, 0xA8, 0x70, 0x00}, // w
			new byte[] {0x00, 0x00, 0x88, 0x50, 0x20, 0x50, 0x88, 0x00}, // x
			new byte[] {0x00, 0x00, 0x88, 0x88, 0x98, 0x68, 0x08, 0x70}, // y
			new byte[] {0x00, 0x00, 0xF8, 0x10, 0x20, 0x40, 0xF8, 0x00}, // z
			new byte[] {0x10, 0x20, 0x20, 0x40, 0x20, 0x20, 0x10, 0x00}, // {
			new byte[] {0x00, 0x20, 0x10, 0xF8, 0x10, 0x20, 0x00, 0x00}, // | (sto)
			new byte[] {0x40, 0x20, 0x20, 0x10, 0x20, 0x20, 0x40, 0x00}, // }
			new byte[] {0x76, 0xDC, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00}, // ~
			new byte[] {0x00, 0x10, 0x38, 0x6C, 0xC6, 0xC6, 0xFE, 0x00}, // .
			new byte[] {0x3E, 0x60, 0xC0, 0x60, 0x3E, 0x08, 0x04, 0x18}, // .
			new byte[] {0x00, 0x48, 0x00, 0xCC, 0xCC, 0xCC, 0xCC, 0x76}, // .
			new byte[] {0x18, 0x20, 0x00, 0x78, 0xCC, 0xFC, 0xC0, 0x78}, // .
			new byte[] {0x10, 0x28, 0x00, 0x78, 0x0C, 0x7C, 0xCC, 0x76}, // .
			new byte[] {0x00, 0x48, 0x00, 0x78, 0x0C, 0x7C, 0xCC, 0x76}, // .
			new byte[] {0x30, 0x08, 0x00, 0x78, 0x0C, 0x7C, 0xCC, 0x76}, // .
			new byte[] {0x48, 0x30, 0x00, 0x78, 0x0C, 0x7C, 0xCC, 0x76}, // .
			new byte[] {0x78, 0xCC, 0xC0, 0xCC, 0x78, 0x10, 0x08, 0x30}, // .
			new byte[] {0x30, 0x48, 0x84, 0x78, 0xCC, 0xFC, 0xC0, 0x78}, // .
			new byte[] {0x00, 0x48, 0x00, 0x78, 0xCC, 0xFC, 0xC0, 0x78}, // .
			new byte[] {0x30, 0x08, 0x00, 0x78, 0xCC, 0xFC, 0xC0, 0x78}, // .
			new byte[] {0x00, 0x48, 0x00, 0x30, 0x30, 0x30, 0x30, 0x30}, // .
			new byte[] {0x30, 0x48, 0x00, 0x30, 0x30, 0x30, 0x30, 0x30}, // .
			new byte[] {0x60, 0x10, 0x00, 0x30, 0x30, 0x30, 0x30, 0x30}, // .
			new byte[] {0x48, 0x00, 0x30, 0x78, 0xCC, 0xCC, 0xFC, 0xCC}, // .
			new byte[] {0x30, 0x48, 0x30, 0x48, 0x84, 0xFC, 0x84, 0x84}, // .
			new byte[] {0x18, 0x20, 0x00, 0xF8, 0x80, 0xF0, 0x80, 0xF8}, // .
			new byte[] {0x00, 0x00, 0x00, 0x66, 0x19, 0x77, 0x88, 0x77}, // .
			new byte[] {0x00, 0x00, 0x00, 0x0F, 0x14, 0x3E, 0x44, 0x87}, // .
			new byte[] {0x30, 0x48, 0x84, 0x78, 0xCC, 0xCC, 0xCC, 0x78}, // .
			new byte[] {0x00, 0x48, 0x00, 0x78, 0xCC, 0xCC, 0xCC, 0x78}, // .
			new byte[] {0x60, 0x10, 0x00, 0x78, 0xCC, 0xCC, 0xCC, 0x78}, // .
			new byte[] {0x30, 0x48, 0x84, 0x00, 0xCC, 0xCC, 0xCC, 0x76}, // .
			new byte[] {0x60, 0x10, 0x00, 0xCC, 0xCC, 0xCC, 0xCC, 0x76}, // .
			new byte[] {0x48, 0x00, 0xCC, 0xCC, 0xCC, 0x7C, 0x0C, 0xF8}, // .
			new byte[] {0x44, 0x00, 0x38, 0x6C, 0xC6, 0xC6, 0x6C, 0x38}, // .
			new byte[] {0x24, 0x00, 0x66, 0x66, 0x66, 0x66, 0x66, 0x3C}, // .
			new byte[] {0x00, 0x08, 0x1C, 0x28, 0x28, 0x1C, 0x08, 0x00}, // .
			new byte[] {0x1C, 0x22, 0x20, 0x70, 0x20, 0x22, 0x5C, 0x00}, // .
			new byte[] {0x44, 0x28, 0x10, 0x10, 0x38, 0x10, 0x38, 0x10}, // .
			new byte[] {0xF0, 0x88, 0x8A, 0xF7, 0x82, 0x82, 0x83, 0x00}, // .
			new byte[] {0x06, 0x08, 0x08, 0x3C, 0x10, 0x10, 0x60, 0x00}, // .
			new byte[] {0x18, 0x20, 0x00, 0x78, 0x0C, 0x7C, 0xCC, 0x76}, // .
			new byte[] {0x18, 0x20, 0x00, 0x30, 0x30, 0x30, 0x30, 0x30}, // .
			new byte[] {0x18, 0x20, 0x00, 0x78, 0xCC, 0xCC, 0xCC, 0x78}, // .
			new byte[] {0x18, 0x20, 0x00, 0xCC, 0xCC, 0xCC, 0xCC, 0x76}, // .
			new byte[] {0x80, 0x78, 0x04, 0xF8, 0xCC, 0xCC, 0xCC, 0xCC}, // .
			new byte[] {0x80, 0x7E, 0x01, 0xC6, 0xE6, 0xD6, 0xCE, 0xC6}, // .
			new byte[] {0x00, 0x78, 0x0C, 0x7C, 0xCC, 0x76, 0x00, 0xFE}, // .
			new byte[] {0x00, 0x78, 0xCC, 0xCC, 0xCC, 0x78, 0x00, 0xFC}, // .
			new byte[] {0x00, 0x00, 0x18, 0x18, 0x30, 0x60, 0x66, 0x3C}, // .
			new byte[] {0xFF, 0x80, 0x80, 0x80, 0x00, 0x00, 0x00, 0x00}, // .
			new byte[] {0xFF, 0x01, 0x01, 0x01, 0x00, 0x00, 0x00, 0x00}, // .
			new byte[] {0x40, 0xC4, 0x48, 0x50, 0x26, 0x49, 0x82, 0x07}, // .
			new byte[] {0x40, 0xC4, 0x48, 0x50, 0x26, 0x4A, 0x9F, 0x02}, // .
			new byte[] {0x00, 0x30, 0x00, 0x30, 0x30, 0x30, 0x30, 0x30}, // .
			new byte[] {0x00, 0x12, 0x24, 0x48, 0x90, 0x48, 0x24, 0x12}, // .
			new byte[] {0x00, 0x48, 0x24, 0x12, 0x09, 0x12, 0x24, 0x48}, // .
			new byte[] {0x49, 0x00, 0x92, 0x00, 0x49, 0x00, 0x92, 0x00}, // .
			new byte[] {0x6D, 0x00, 0xB6, 0x00, 0x6D, 0x00, 0xB6, 0x00}, // .
			new byte[] {0xAA, 0x55, 0xAA, 0x55, 0xAA, 0x55, 0xAA, 0x55}, // .
			new byte[] {0x10, 0x10, 0x10, 0x10, 0x10, 0x10, 0x10, 0x10}, // .
			new byte[] {0x10, 0x10, 0x10, 0x10, 0xF0, 0x10, 0x10, 0x10}, // .
			new byte[] {0x10, 0x10, 0x10, 0xF0, 0x10, 0xF0, 0x10, 0x10}, // .
			new byte[] {0x28, 0x28, 0x28, 0x28, 0xE8, 0x28, 0x28, 0x28}, // .
			new byte[] {0x00, 0x00, 0x00, 0x00, 0xF8, 0x28, 0x28, 0x28}, // .
			new byte[] {0x00, 0x00, 0x00, 0xF0, 0x10, 0xF0, 0x10, 0x10}, // .
			new byte[] {0x28, 0x28, 0x28, 0xE8, 0x08, 0xE8, 0x28, 0x28}, // .
			new byte[] {0x28, 0x28, 0x28, 0x28, 0x28, 0x28, 0x28, 0x28}, // .
			new byte[] {0x00, 0x00, 0x00, 0xF8, 0x08, 0xE8, 0x28, 0x28}, // .
			new byte[] {0x28, 0x28, 0x28, 0xE8, 0x08, 0xF8, 0x00, 0x00}, // .
			new byte[] {0x28, 0x28, 0x28, 0x28, 0xF8, 0x00, 0x00, 0x00}, // .
			new byte[] {0x10, 0x10, 0x10, 0xF0, 0x10, 0xF0, 0x00, 0x00}, // .
			new byte[] {0x00, 0x00, 0x00, 0x00, 0xF0, 0x10, 0x10, 0x10}, // .
			new byte[] {0x10, 0x10, 0x10, 0x10, 0x1F, 0x00, 0x00, 0x00}, // .
			new byte[] {0x10, 0x10, 0x10, 0x10, 0xFF, 0x00, 0x00, 0x00}, // .
			new byte[] {0x00, 0x00, 0x00, 0x00, 0xFF, 0x10, 0x10, 0x10}, // .
			new byte[] {0x10, 0x10, 0x10, 0x10, 0x1F, 0x10, 0x10, 0x10}, // .
			new byte[] {0x00, 0x00, 0x00, 0x00, 0xFF, 0x00, 0x00, 0x00}, // .
			new byte[] {0x10, 0x10, 0x10, 0x10, 0xFF, 0x10, 0x10, 0x10}, // .
			new byte[] {0x10, 0x10, 0x10, 0x1F, 0x10, 0x1F, 0x10, 0x10}, // .
			new byte[] {0x28, 0x28, 0x28, 0x28, 0x3F, 0x28, 0x28, 0x28}, // .
			new byte[] {0x28, 0x28, 0x28, 0x2F, 0x20, 0x3F, 0x00, 0x00}, // .
			new byte[] {0x00, 0x00, 0x00, 0x3F, 0x20, 0x2F, 0x28, 0x28}, // .
			new byte[] {0x28, 0x28, 0x28, 0xEF, 0x00, 0xFF, 0x00, 0x00}, // .
			new byte[] {0x00, 0x00, 0x00, 0xFF, 0x00, 0xEF, 0x28, 0x28}, // .
			new byte[] {0x28, 0x28, 0x28, 0x2F, 0x20, 0x2F, 0x28, 0x28}, // .
			new byte[] {0x00, 0x00, 0x00, 0xFF, 0x00, 0xFF, 0x00, 0x00}, // .
			new byte[] {0x28, 0x28, 0x28, 0xEF, 0x00, 0xEF, 0x28, 0x28}, // .
			new byte[] {0x10, 0x10, 0x10, 0xFF, 0x00, 0xFF, 0x00, 0x00}, // .
			new byte[] {0x28, 0x28, 0x28, 0x28, 0xFF, 0x00, 0x00, 0x00}, // .
			new byte[] {0x00, 0x00, 0x00, 0xFF, 0x00, 0xFF, 0x10, 0x10}, // .
			new byte[] {0x00, 0x00, 0x00, 0x00, 0xFF, 0x28, 0x28, 0x28}, // .
			new byte[] {0x28, 0x28, 0x28, 0x28, 0x3F, 0x00, 0x00, 0x00}, // .
			new byte[] {0x10, 0x10, 0x10, 0x1F, 0x10, 0x1F, 0x00, 0x00}, // .
			new byte[] {0x00, 0x00, 0x00, 0x1F, 0x10, 0x1F, 0x10, 0x10}, // .
			new byte[] {0x00, 0x00, 0x00, 0x00, 0x3F, 0x28, 0x28, 0x28}, // .
			new byte[] {0x28, 0x28, 0x28, 0x28, 0xFF, 0x28, 0x28, 0x28}, // .
			new byte[] {0x10, 0x10, 0x10, 0xFF, 0x10, 0xFF, 0x10, 0x10}, // .
			new byte[] {0x10, 0x10, 0x10, 0x10, 0xF0, 0x00, 0x00, 0x00}, // .
			new byte[] {0x00, 0x00, 0x00, 0x00, 0x1F, 0x10, 0x10, 0x10}, // .
			new byte[] {0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF, 0xFF}, // .
			new byte[] {0x00, 0x00, 0x00, 0x00, 0xFF, 0xFF, 0xFF, 0x00}, // .
			new byte[] {0xE0, 0xE0, 0xE0, 0xE0, 0xE0, 0xE0, 0xE0, 0xE0}, // .
			new byte[] {0x07, 0x07, 0x07, 0x07, 0x07, 0x07, 0x07, 0x07}, // .
			new byte[] {0xFF, 0xFF, 0xFF, 0xFF, 0x00, 0x00, 0x00, 0x00}, // .
			new byte[] {0x00, 0x00, 0x00, 0x02, 0x34, 0x4C, 0x4C, 0x32}, // .
			new byte[] {0x00, 0x5C, 0x22, 0x22, 0x3C, 0x44, 0x44, 0x78}, // .
			new byte[] {0x7E, 0x42, 0x42, 0x40, 0x40, 0x40, 0x40, 0x40}, // .
			new byte[] {0x00, 0x00, 0x02, 0x7C, 0xA8, 0x28, 0x28, 0x44}, // .
			new byte[] {0x00, 0x7E, 0x61, 0x30, 0x18, 0x08, 0x10, 0x20}, // .
			new byte[] {0x00, 0x00, 0x08, 0x7F, 0x88, 0x88, 0x88, 0x70}, // .
			new byte[] {0x00, 0x00, 0x00, 0x22, 0x44, 0x44, 0x7A, 0x80}, // .
			new byte[] {0x00, 0x00, 0x00, 0x7C, 0x10, 0x10, 0x10, 0x10}, // .
			new byte[] {0x00, 0x1C, 0x08, 0x3E, 0x41, 0x41, 0x41, 0x3E}, // .
			new byte[] {0x00, 0x00, 0x38, 0x44, 0x44, 0x7C, 0x44, 0x44}, // .
			new byte[] {0x3C, 0x66, 0xC3, 0xC3, 0xC3, 0x66, 0x24, 0x66}, // .
			new byte[] {0x0C, 0x10, 0x08, 0x1C, 0x22, 0x22, 0x22, 0x1C}, // .
			new byte[] {0x00, 0x00, 0x00, 0x00, 0x6C, 0x92, 0x92, 0x6C}, // .
			new byte[] {0x00, 0x01, 0x1A, 0x26, 0x2A, 0x32, 0x2C, 0x40}, // .
			new byte[] {0x00, 0x18, 0x20, 0x20, 0x30, 0x20, 0x20, 0x18}, // .
			new byte[] {0x00, 0x3C, 0x42, 0x42, 0x42, 0x42, 0x42, 0x42}, // .
			new byte[] {0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0xA8, 0x00}, // .
			new byte[] {0x00, 0x08, 0x08, 0x3E, 0x08, 0x08, 0x00, 0x3E}, // .
			new byte[] {0x00, 0x10, 0x08, 0x04, 0x08, 0x10, 0x00, 0x3E}, // .
			new byte[] {0x00, 0x04, 0x08, 0x10, 0x08, 0x04, 0x00, 0x3E}, // .
			new byte[] {0x00, 0x06, 0x09, 0x09, 0x08, 0x08, 0x08, 0x00}, // .
			new byte[] {0x00, 0x00, 0x08, 0x08, 0x08, 0x48, 0x48, 0x30}, // .
			new byte[] {0x00, 0x00, 0x08, 0x00, 0x3E, 0x00, 0x08, 0x00}, // .
			new byte[] {0x00, 0x60, 0x92, 0x0C, 0x60, 0x92, 0x0C, 0x00}, // .
			new byte[] {0x60, 0x90, 0x60, 0x00, 0x00, 0x00, 0x00, 0x00}, // .
			new byte[] {0x00, 0x00, 0x00, 0x30, 0x78, 0x30, 0x00, 0x00}, // .
			new byte[] {0x00, 0x00, 0x00, 0x00, 0x20, 0x00, 0x00, 0x00}, // .
			new byte[] {0x00, 0x03, 0x04, 0x04, 0xC8, 0x28, 0x10, 0x00}, // .
			new byte[] {0x00, 0x00, 0x00, 0x7C, 0x42, 0x42, 0x42, 0x00}, // .
			new byte[] {0x18, 0x24, 0x08, 0x10, 0x3C, 0x00, 0x00, 0x00}, // .
			new byte[] {0x00, 0x00, 0x00, 0x3E, 0x3E, 0x3E, 0x3E, 0x00}, // .
			new byte[] {0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00, 0x00}  // .
		};
	}
}
