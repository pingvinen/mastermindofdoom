Knuth pseudo-code
=================

4 slots
options from 0-7

0 = green
1 = blue
2 = brown
3 = orange
4 = yellow
5 = black
6 = white
7 = red

1: 0 0 1 1 = ---
2: 2 2 3 3 = V
3: 2 2 4 4 = V
4: 2 5 6 7 = PP
5: 4 3 6 7 = PPVV
6: 3 4 6 7 = PPPP

1: 0 0 1 1 = P
2: 0 0 2 2 = ---
3: 3 3 1 4 = PV
4: 4 4 1 5 = VV
5: 3 5 6 1 = PPP
6: 3 5 7 1 = PPPP


1: 0 0 1 1 = PP
    - two 0's are correct
    - two 1's are correct
    - one 0 and one 1 are correct
2: 0 0 2 2 = PPP
    - we know: one 0 is correct
    - at least one 2 is correct
3: 0 0 2 3 = PPP
    - seems likely that 002x is correct
4: 4 4 2 3 = P
    - seems very likely that 002x is correct
    - we know that x is not 4
5: 0 0 2 5 = PPP
    - not black
6: 0 0 2 6 = PPPP



real: 210

0 0 1 = VV
