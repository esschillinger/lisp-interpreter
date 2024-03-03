(define append (list a) (cond
	(nil? list) (cons a ())
	1 (cons (car list) (append (cdr list) a))
))							; function "append" defined

(1 2 3) 						; (1.(2.(3.())))
(cons (car (1 2 3)) (append (2 3) 4)) 			; (1.(2.(3.(4.()))))
(append (1 2 3) 4) 					; (1.(2.(3.(4.()))))
(set mylist (append (1 2 3) 4))				; mylist = (1.(2.(3.(4.()))))
(car mylist)						; 1
(cdr mylist)						; (2.(3.(4.())))
(set newlist (append mylist 5))				; newlist = (1.(2.(3.(4.(5.())))))
(car newlist)						; 1
(car (cdr newlist))					; 2
(cdr (cdr newlist))					; (3.(4.(5.())))						



(nil? ())						; True
(nil? (and? 5 ()))					; True
(nil? (or? () 5))					; False
(number? 2)						; True
(number? (+ 9 (* 2 45)))				; True
(number? (1 2 3))					; False
(symbol? append)					; True
(symbol? (append newlist 6))				; False
(list? newlist)						; False (even though it evaluates to a list, it is a symbol)
(list? (1))						; True
(list? ())						; True (not an atom)
(list? 1)						; False



(eq? (* 2 4) (+ 4 4))					; True
(eq? (8 1 0 4) (8 1 0 4))				; True
(eq? (8 1 0 4) (8 1 0 4 ()))				; False
(eq? newlist mylist)					; False
(eq? 1 (1))						; False



(define increment (a) (+ a 1))				; function "increment" defined
(set num 2)						; num = 2
(increment num)						; 3
(+ num (+ 3 6))						; 11
(set num (increment num))				; num = 3
(+ num (+ 3 6))						; 12



;(- 314 (* 10 (+ 15 (/ (* 5 9) 3)))) ; should be 14


; (cons (1 2 3) 4) ; 	-> ((1.(2.(3.()))).4)

;(set temp (4))	; -> temp = (4.())
