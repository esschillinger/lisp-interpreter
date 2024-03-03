; To run, enter "Lisp.exe 1_comparison-types.lsp" in a terminal (located in this directory)
; Note that '=' can be used interchangeably with 'eq?'

(nil? (eq? 0 ()))                               ; True
(eq? 1 1)                                       ; True
(eq? (1) (1))                                   ; True
(nil? (eq? 1 (1)))                              ; True
(eq? (cons 1 ()) (1))                           ; True
(nil? (eq? (cons 1 2) (1 2)))                   ; True
(eq? (1 2 8 5 10 93) (1 2 8 5 10 93))           ; True
(nil? (eq? (1 2 8 5 10 93) (1 2 8 5 10 93 0)))  ; True
(nil? (eq? (1 2 8 () 10 93) (1 2 8 5 10 93)))   ; True
(nil? (eq? (9 0 2 1 0) (6 0 0 6 7)))            ; True
(eq? (+ 15 8) 23)                               ; True
(eq? (1 2 3) (1 2 (+ 1 2)))                     ; True

; Not exhaustively testing nested/complex arithmetic here because that has been done in arithmetic.lsp

(> 9 8)                                         ; True
(nil? (> 8 9))                                  ; True
(> (+ 4 1) 4)                                   ; True
(nil? (> 1 1))                                  ; True

(nil? (< 9 8))                                  ; True
(< 8 9)                                         ; True
(nil? (< (+ 4 1) 4))                            ; True
(nil? (< 1 1))                                  ; True

(and? 1 2)                                      ; True
(nil? (and? 4 ()))                              ; True
(nil? (and? () ()))                             ; True
(and? (8) (9))                                  ; True
(and? 0 0)                                      ; True
(nil? (and? 1 (cdr (cons 1 ()))))               ; True

(or? 1 2)                                       ; True
(or? 4 ())                                      ; True
(nil? (or? () ()))                              ; True
(or? (8) (9))                                   ; True
(or? 0 0)                                       ; True
(or? 1 (cdr (cons 1 ())))                       ; True

(nil? (nil? 0))                                 ; True
(nil? (nil? 1))                                 ; True
(nil? ())                                       ; True
(nil? (nil? (0)))                               ; True
(nil? (car (cons () 1)))                        ; True

(nil? (number? hello))                          ; True
(number? 0)                                     ; True
(number? 314159265)                             ; True
(nil? (number? ()))                             ; True
(number? (car (1)))                             ; True

(symbol? bruh)                                  ; True
(nil? (symbol? 1))                              ; True
(nil? (symbol? ()))                             ; True
(nil? (symbol? (+ 1 1)))                        ; True
(nil? (symbol? (1 2 3)))                        ; True
(symbol? and?)                                  ; True

(nil? (list? ()))                               ; True
(nil? (list? (+ 1 2)))                          ; True
(list? ((+ 1 2) 3 4))                           ; True
(list? (0))                                     ; True
(list? (cons 1 1))                              ; True
(list? (cons () ()))                            ; True