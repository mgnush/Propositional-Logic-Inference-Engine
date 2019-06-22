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

 -------
| Notes |
 -------
* TT Is implemented using breadth-first approach, counting true models
     and models where (KB => a) is valid, instead of depth-first.