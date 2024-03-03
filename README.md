# lisp-interpreter
Fully functioning Lisp interpreter that I created for CS 503 (Programming Languages). The version of Lisp used here is a modified version of Common Lisp that was used for the purposes of this class. Test cases have been created to assess each feature required, as specified by the Lisp Spec sheet posted to Blackboard. These include: arithmetic expressions, variable assignment, function definition, function calls, and comparison expressions.

Test cases have been grouped into the Scanner, Parser, and Interpreter directories, depending upon what aspect of the project they aim to test. They have been further subdivided into files according to the categories mentioned in the previous section. Each test file contains the code’s output, stored in comments on the lines from which it was generated. Note that output has been formatted to be as readable as possible, which lends itself to adjustments like the following:

All comparison expressions have been created to output “True” for greater readability. For example, we would take the expression
(eq? (* 2 7) (- 14 1))		; False
and write it as
(nil? (eq? (* 2 7) (- 14 1)))	; True
so that there is not a mix of “True”s and “False”s that must be aligned with the output, should you choose to run the files.

The “project source” directory contains the solution file, project files, source code, and all other information that Visual Studio requires to run the project. In essence, it is the complete project/interpreter.
For ease of grading, however, the test case files and executables have been copied into the “test” directory, and the source code files have been copied into the “source code” directory. Thus, the “project source” directory can largely be ignored for your purposes. To run any test case, open the test case file and follow the instructions listed at the top that have been commented.
