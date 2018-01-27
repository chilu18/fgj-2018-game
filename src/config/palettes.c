#include "palettes.h"
#include "../../nes.h"

const unsigned char PALETTE_SPRITE_1[]={
  PALETTE_COLOR_DARK_GRAY, // PSD Light gray
  PALETTE_COLOR_BLACK, // PSD Black
  PALETTE_COLOR_LIGHT_GRAY // PSD Skin
};

const unsigned char PALETTE_BG_1[]={
  PALETTE_COLOR_BLACK,
  PALETTE_COLOR_DARK_GRAY,
  PALETTE_COLOR_LIGHT_GRAY
};

void loadDefaultPalettes (void) {
  int index;
  SET_PPU_ADDR(PPU_ADDR_PALETTE_BACKGROUND_0);
  for (index = 0; index < sizeof(PALETTE_BG_1); ++index) {
    PPU_VRAM_IO = PALETTE_BG_1[index];
  }
  SET_PPU_ADDR(PPU_ADDR_PALETTE_SPRITE_0);
  for (index = 0; index < sizeof(PALETTE_SPRITE_1); ++index) {
    PPU_VRAM_IO = PALETTE_SPRITE_1[index];
  }
  SET_PPU_ADDR_VALUE(PPU_ADDR_PALETTE_UNIVERSAL_BACKGROUND_COLOR, PALETTE_COLOR_BLACK);
}