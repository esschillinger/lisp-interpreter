; Sample program--output from PARSER pasted below
; To verify results, run "LispParser.exe sexprs.lsp" from the command line (while in the correct directory)


(define exists (list elem) (cond
    (nil? list)             ()
    (eq? (car list) elem)   1
    1                       (exists (cdr list) elem)
))

(1 2 3)

(+ 1 1)

()

1

hello

dr

yessick



; sexpr type : Sexpr+ListExpr
; (define  exists  (list  elem ) (cond  (nil?  list ) () (eq?  (car  list ) elem ) 1  1  (exists  (cdr  list ) elem )))

; sexpr type : Sexpr+ListExpr
; (1  2  3 )

; sexpr type : Sexpr+ListExpr
; (+  1  1 )

; sexpr type : Sexpr+Nil
; ()

; sexpr type : Sexpr+Atom
; 1

; sexpr type : Sexpr+Atom
; hello

; sexpr type : Sexpr+Atom
; dr

; sexpr type : Sexpr+Atom
; yessick
