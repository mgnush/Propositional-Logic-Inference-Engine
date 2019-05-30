 ------------------
| Inference Engine |
 ------------------
@Author Magnus Hjorth (100669317)

Launch program by command line:
iengine method filename

*method: TT, FC, BC
*filename: path of input file

 ----------
| Features |
 ----------
* Truth-Table (TT)
* Forward Chaining (FC)
* Backward Chaining (BC)

 ------------
| Test cases |
 ------------
Only some of the test cases used are listed here.

Cases with deliberately not-entailed combinations of kb's and query:
* TELL
  p2=> p3; c => e; b&e => f; f&g => h; p1=>d; p1&p3 => c; a; b; p2;
  ASK
  d

Cases with deliberately entailed queries:
* TELL
  p2=> p3; c => e; b&e => f; f&g => h; p1=>d; p1&p3 => c; a; b; p2; p1;
  ASK
  d


 ----------------------------
| Acknowledgements/Resources |
 ----------------------------
The only resources that have been referred to in the making
of this program are lecture slides:
* COS30019_Lecture_07-2spp.pdf Page 10-15 for forward chaining,
  particularly the forward chaining algorithm pseudo-code on page 15.
* COS30019_Lecture_07-2spp.pdf Page 15-20 for backward chaining.

Both examples on the slides assisted me in understanding the gist
of the algorithms.
 -------
| Notes |
 -------
* TT