# ReelSet
Reel set component for slot-style games in Unity3D

There have been a number of reel set components (or systems may be a better word) that I have worked with and all seem to have a lack of extensibility and ease of use. The short story is adding a feature to the reels means adding more logic to the reels class. It is not uncommon to see a reels class be upwards of 5000 lines of code. This is terrible design and is rife in the slot machine industry. For example, let's take one of the most common features of a reel spin - the anticipation of a symbol landing. In most cases you might want to increase the speed and length of the reel spin. In every system I have seen, the logic follows in the reels class as "if (anticipation) blah blah". And with every new feature the horrible code continues. 

The purpose of this project is to overcome those issues and design a more flexible and usable reels component.
