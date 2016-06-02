
all : MBAlertView/.git

MBAlertView/.git :
	git clone https://github.com/skela/MBAlertView.git MBAlertView
	(cd MBAlertView && git --git-dir=.git checkout e779ba3235aefdbe376f3b95b369d6b84327405b)

clean :
	rm -rf MBAlertView
