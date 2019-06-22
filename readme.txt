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
* Truth-Table (TT) - Only works with HornKB
* Forward Chaining (FC)
* Backward Chaining (BC)

 ------------
| Test cases |
 ------------
Only some of the test cases used are listed here.

Entailed
* TELL
  p2=> p3; p3 => p1; c => e; b&e => f; f&g => h; p1=>d; p1&p3 => c; a; b; p2;
  ASK
  d
* TELL
  p2=> p3; p3 => p1; c => e; b&e => f; f&g => h; p1=>d; p1&p3 => c; a; b; p2;
  ASK
  f
* TELL
  P=> Q; L&M => P; B&L => M; A&P => L; A&B => L; A; B;
  ASK
  Q

Not Entailed
* TELL
  P=> Q; L&M => P; B&L => M; A&P => L; A&B => L; A;
  ASK
  Q
* TELL
  p2=> p3; p3 => p1; c => e; b&e => f; f&g => h; p1=>d; p1&p3 => c; a; b; p2;
  ASK
  g

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
* TT Is implemented using breadth-first approach, counting true models
     and models where (KB => a) is valid, instead of depth-first.