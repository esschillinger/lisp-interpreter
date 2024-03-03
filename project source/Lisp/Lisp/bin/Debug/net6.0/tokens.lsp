; Sample program--output from SCANNER pasted below
; To verify results, run "LispScanner.exe tokens.lsp" from the command line (while in the correct directory)


(define exists (list elem) (cond
    (nil? list)             ()
    (eq? (car list) elem)   1
    1                       (exists (cdr list) elem)
))


; LEFT_PAREN (
; SYMBOL define
; SYMBOL exists
; LEFT_PAREN (
; SYMBOL list
; SYMBOL elem
; RIGHT_PAREN )
; LEFT_PAREN (
; SYMBOL cond
; LEFT_PAREN (
; SYMBOL nil?
; SYMBOL list
; RIGHT_PAREN )
; NIL ()
; LEFT_PAREN (
; SYMBOL eq?
; LEFT_PAREN (
; SYMBOL car
; SYMBOL list
; RIGHT_PAREN )
; SYMBOL elem
; RIGHT_PAREN )
; NUM 1 1
; NUM 1 1
; LEFT_PAREN (
; SYMBOL exists
; LEFT_PAREN (
; SYMBOL cdr
; SYMBOL list
; RIGHT_PAREN )
; SYMBOL elem
; RIGHT_PAREN )
; RIGHT_PAREN )
; RIGHT_PAREN )
; EOF