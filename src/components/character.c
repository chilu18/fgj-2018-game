#include "character.h"
#include "../../nes.h"
#include "../nes/sprite.h"
#include "../nes/controller.h"
#include "../nes/sounds.h"
#include "./utils/math.h"

struct Vector2 playerPosition;

void updateCharacterPos (void) {
  if (Input.player1.Right) {
    ++playerPosition.x;
    Sprites[1].flags = Sprites[0].flags &= ~SPRITE_FLAG_FLIP_HORIZONTALLY;
  } else if (Input.player1.Left) {
    --playerPosition.x;
    Sprites[1].flags = Sprites[0].flags |= SPRITE_FLAG_FLIP_HORIZONTALLY;
  }

  if (Input.player1.Up) {
    --playerPosition.y;
  } else if (Input.player1.Down) {
    ++playerPosition.y;
  }

  if (Input.player1.A) {
    playSample(0);
  }
 /* Sprites[1].xPos = Sprites[0].xPos = playerPosition.x + 0x80;
  Sprites[0].yPos = playerPosition.y + 0x80;
  Sprites[1].yPos = playerPosition.y + 0x88;*/
}

void updateCharacter(void) {
  updateCharacterPos();
}

void drawCharacter(void) {
  Sprites[0].xPos = 0x80;
  Sprites[0].yPos = 0x80;
  Sprites[0].spriteIndex = 0x01;
  Sprites[0].flags = 0x00;

  Sprites[1].xPos = 0x80;
  Sprites[1].yPos = 0x88;
  Sprites[1].spriteIndex = 0x11;
  Sprites[1].flags = 0x00;

  Sprites[2].xPos = 0x78;
  Sprites[2].yPos = 0x80;
  Sprites[2].spriteIndex = 0x00;
  Sprites[2].flags = 0x00;

  Sprites[3].xPos = 0x78;
  Sprites[3].yPos = 0x88;
  Sprites[3].spriteIndex = 0x10;
  Sprites[3].flags = 0x00;
}

void startCharacter(void) {
  drawCharacter();
}
