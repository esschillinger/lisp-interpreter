; To run, enter "Lisp.exe 1_vars-functions.lsp" in a terminal (located in this directory)

(define append (list a) (cond
	(nil? list) (cons a ())
	1 (cons (car list) (append (cdr list) a))
))                                                                          ; function "append" defined.

(define exists? (list elem) (cond
    (nil? list)             ()
    (eq? (car list) elem)   1
    1                       (exists? (cdr list) elem)                       ; function "exists?" defined.
))

(set mylist (1 2 3))                                                        ; variable "mylist" set. NOTE: mylist = (1.(2.(3.())))
(eq? (1 2 3 4) (append mylist 4))                                           ; True
(nil? (exists? mylist 4))                                                   ; True

(set mylist (append mylist 4))                                              ; variable "mylist" set. NOTE: mylist = (1.(2.(3.(4.()))))
(= 1 (exists? mylist 4))                                                    ; True



(define janky_int_sqrt (num) (cond
    (nil? (number? num))    ()
    (< num 1)               ()
    1                       (sqrt_helper num 0)
))                                                                          ; function "janky_int_sqrt" defined.

(define sqrt_helper (num iter) (cond
    (= (* iter iter) num)   iter
    (> iter (/ num 2))      ()
    1                       (sqrt_helper num (+ iter 1))
))                                                                          ; function "sqrt_helper" defined.

(= 4 (janky_int_sqrt (* 8 2)))                                              ; True
(nil? (janky_int_sqrt 15))                                                  ; True
(nil? (janky_int_sqrt mylist))                                              ; True
