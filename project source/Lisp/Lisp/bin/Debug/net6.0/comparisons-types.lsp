; Note that '=' can be used interchangeably with 'eq?'

(eq? 0 ())                                  ; False
(eq? 1 1)                                   ; True
(eq? (1) (1))                               ; True
(eq? 1 (1))                                 ; False
(eq? (cons 1 ()) (1))                       ; True
(eq? (cons 1 2) (1 2))                      ; False
(eq? (1 2 8 5 10 93) (1 2 8 5 10 93))       ; True
(eq? (1 2 8 5 10 93) (1 2 8 5 10 93 0))     ; False
(eq? (1 2 8 () 10 93) (1 2 8 5 10 93))      ; False
(eq? (9 0 2 1 0) (6 0 0 6 7))               ; False
(eq? (+ 15 8) 23)                           ; True
(eq? (1 2 3) (1 2 (+ 1 2)))                 ; True

; Not exhaustively testing nested/complex arithmetic here because that has been done in arithmetic.lsp

(> 9 8)                                     ; True
(> 8 9)                                     ; False
(> (+ 4 1) 4)                               ; True
(> 1 1)                                     ; False

(< 9 8)                                     ; False
(< 8 9)                                     ; True
(< (+ 4 1) 4)                               ; False
(< 1 1)                                     ; False

(and? 1 2)                                  ; True
(and? 4 ())                                 ; False
(and? () ())                                ; False
(and? (8) (9))                              ; True
(and? 0 0)                                  ; True
(and? 1 (cdr (cons 1 ())))                  ; False

(or? 1 2)                                   ; True
(or? 4 ())                                  ; True
(or? () ())                                 ; False
(or? (8) (9))                               ; True
(or? 0 0)                                   ; True
(or? 1 (cdr (cons 1 ())))                   ; True

(nil? 0)                                    ; False
(nil? 1)                                    ; False
(nil? ())                                   ; True
(nil? (0))                                  ; False
(nil? (car (cons () 1)))                    ; True

(number? hello)                             ; False
(number? 0)                                 ; True
(number? 314159265)                         ; True
(number? ())                                ; False
(number? (car (1)))                         ; True

(symbol? bruh)                              ; True
(symbol? 1)                                 ; False
(symbol? ())                                ; False
(symbol? (+ 1 1))                           ; False
(symbol? (1 2 3))                           ; False
(symbol? and?)                              ; True

(list? ())                                  ; False
(list? (+ 1 2))                             ; False
(list? ((+ 1 2) 3 4))                       ; True
(list? (0))                                 ; True
(list? (cons 1 1))                          ; True
(list? (cons () ()))                        ; True