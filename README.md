# Omnipede
Omnipede is an open source chess engine designed for use in [infinite chess](https://www.infinitechess.org/).
## How to use
1. Press the "Copy Game" button in the pause menu
2. Paste the game into omnipede-input.json
3. Run omnipede.exe
```
Config Loaded:
     Depth: 2
     File Path: .\omnipede-input.json
Position Decoded
Starting Search For "Best" Move
Move Found:
     Position Value: 0
     Add: {"xCoord":4,"yCoord":5,"type":"pawnsW","hasMoved":true}
     Remove: {"xCoord":4,"yCoord":5,"type":"pawnsB","hasMoved":true}
     Remove: {"xCoord":5,"yCoord":4,"type":"pawnsW","hasMoved":true}
Total Run Time: 00:00:00.7679507
```
4. Find `Add:` and `Remove:` in the output
    1. `Add:` has the position of where you will move the pice to
    2. One `Remove:` has the position of the piece you want to move
    3. The other `Remove:` has the position of the piece you are capturing
5. Move the piece from the `Remove:` postion to the `Add:` position
      1. You can turn on perspective mode and look strat down to acuratly find the positions on the board
      2. Match the text after `"type":` with the `Add:` to know wich `Remove` you should be moving the piece from
6. Press any key to close the application
## Things that don't work (yet)
- It doesn't know that pawns can premote
- It assumes that it is always playing Limit 7
- It might not always obey check rules, but I think as long as depth >=2 it should work
