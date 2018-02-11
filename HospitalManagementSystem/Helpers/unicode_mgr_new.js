var browser = navigator.appName

var unicodeSajha = 'Y';

var numArray = new Array("0", "1", "2", "3", "4", "5", "6", "7", "8", "9");

var charLCArray = new Array("a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z");

var charUCArray = new Array("A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z");

var str = ""; var msg = ""; var icount = 1; var iOpen = 2; var pauseSec = 0; var myFormerText = 'spRck'; var prnCharNpLen = -1;

var numArrayHTML = new Array("०", "१", "२", "३", "४", "५", "६", "७", "८", "९", "Err");

var charLCArrayHTML = new Array("ब", "द", "य", "म", "भ", "ा", "न", "ज", "ष", "व", "प", "ि", "र", "ल", "अ", "उ", "घ", "च", "क", "त", "ग", "ख", "ध", "ह", "थ", "श");

var charUCArrayHTML = new Array("ऽ", "ज्ञ", "क्ष", "्", "ड", "ँ", "‌", " ", "ऋ", "ं", "़", "ी", "फ", "ळ", "इ", "ए", "ण", "ठ", "्र", "ट", "झ", "त्र", "ढ", "छ", "ञ", "ङ");

var refCharDCArray = new Array("f{", "]{", "}{", "\"{", "'{", "]]", "\[\[");

var refCharDCArrayHTML = new Array("ा", "ेर्", "ै", "ू", "ु", "ॆ", "ॄ"); //"ॆ&#2374;"

var refCharDCArray = new Array("f{", "]{", "}{", "\"{", "'{");

var refCharDCArrayHTML = new Array("ा", "े", "ै", "ू", "ु");



var refEspCharArray = new Array("\s", ";", "\.\s", "'", "\"", "]]", "\]", "}", "{", "\[\[", "\[", ":", "\.\.\.", "\.\.", "\.", "\_0", "\&");

var refEspCharAHTML = new Array("\s", "स", "।\s", "ु", "ू", "ॆ", "े", "ै", "र्", "ॄ", "ृ", "ः", "…", "॥", "।", "॰", "ॐ"); //"र्" "॰"

var refEspCharASKIP = new Array("1", "1", "2", "1", "1", "2", "1", "1", "1", "2", "1", "1", "3", "2", "1", "2", "1");

var globalSkip = 0;



var charBold = 0; var charItalic = 0; var charEnglish = 0; var charUnderLine = 0; var charSuperScript = 0; var charSubScript = 0;

var myEditedTextPart = "spRck";



function spRckFoc(myCode, text) {
    endChar = " ";

    var myNewText = text; var msgLastWord = ""; var oldText = ""; var curTailSpPos = -1; var tailParaPos = -1;

    if (prnCharNpLen >= 0 && prnCharNpLen >= charNp.length) { oldText = charNp.substring(0, prnCharNpLen); } else { oldText = ""; }

    msg = text; curTailSpPos = msg.lastIndexOf(" "); tailParaPos = msg.lastIndexOf("\n");

    if (tailParaPos >= curTailSpPos) { curTailSpPos = tailParaPos; };



    if (curTailSpPos >= 0 && curTailSpPos != msg.length) { msgLastWord = msg.substring(curTailSpPos + 1, msg.length) };

    if (tailParaPos < 0 && curTailSpPos < 0) { curTailSpPos = 0; tailParaPos = 0; };

    // First Part of message to be transferred to Unicode Nepali. 

    msg = msg.substring(0, curTailSpPos + 1); myTextLen = msg.length; str = " ";



    if (msg.length >= myEditedTextPart.length) {

        if (myEditedTextPart == msg.substring(0, myEditedTextPart.length)) {
            lastSpacePos = myEditedTextPart.lastIndexOf(" "); lastEolPos = myEditedTextPart.lastIndexOf("\n");

            if (lastEolPos > lastSpacePos) { lastSpacePos = lastEolPos; };

            if (lastSpacePos >= 0) { preEditedText = msg.substring(0, lastSpacePos + 1); unEditedText = msg.substring(lastSpacePos + 1, myTextLen); msg = unEditedText; }
        } else { oldText = ""; }
    } else { oldText = ""; }





    //    if (myEditedTextPart != document.spRcktextForm.myText.value)  { 

    if (myEditedTextPart != "") {

        if (icount <= 1) { icount = 1 }; endChar = msg.substring(msg.length - 1, msg.length);

        if (endChar != " ") { endChar = " "; }

        if (endChar == " ") {



            if (msg.length >= 1) {
                charNp = oldText; skip = 0; sweng = 0; //  if (msg.length >= 1)     {charNp = ""; skip = 0 ; sweng  = 0 ;



                while (msg.substring(0, 1) != "") {

                    msg = msg.substring(0 + skip, msg.length);

                    str = msg.substring(0, 1); str1 = msg.substring(0, 2); str2 = msg.substring(0, 3); str4 = msg.substring(0, 4);

                    str5 = msg.substring(0, 5); str6 = msg.substring(0, 6); str7 = msg.substring(0, 7); str8 = msg.substring(0, 8); sEng = msg.substring(0, 5);

                    if (str1.length >= 2 && msg.length < 2) { str1 = " "; }

                    if (str2.length >= 3 && msg.length < 3) { str2 = " "; }

                    if (str6.length >= 6 && msg.length < 6) { str6 = " "; }

                    if (skip >= 1 && msg.length < 1) { str = " " }

                    msg = msg.substring(1, msg.length); eMsg = msg; skip = 0;

                    if (str4 == "    ") {
                        charNp = charNp + "  "; skip = 3; // &#0009; Invisible Control Character

                    } else if (str == " ") {
                        charNp = charNp + " ";

                    } else if (str8.search(/\&#\d\d\d\d\d\;/) == 0) {
                        charNp = charNp + str8; skip = 7;

                    } else if (str7.search(/\&#\d\d\d\d\;/) == 0) {
                        charNp = charNp + str7; skip = 6;

                    } else if (str6.search(/\&\w\w\w\w\;/) == 0 || str6.search("</sup>") == 0 || str6.search("</sub>") == 0) {
                        charNp = charNp + str6; skip = 5;

                    } else if (str5.search(/\&\w\w\w\;/) == 0 || str5.search("<sup>") == 0 || str5.search("<sub>") == 0) {
                        charNp = charNp + str5; skip = 4;

                    } else if (str4.search(/\&\w\w\w/) == 0) {
                        charNp = charNp + str4; skip = 3;

                    } else if (str4.search(/of\]\]/) == 0) {
                        charNp = charNp + "ऒ"; skip = 3;// Short Aa+oKar "&#2322;"; "&#2374;";  



                        // note this: if charCode is > 127, then, write in html code format.

                        //  } else if (str.charCodeAt(0) > "127") {         charNp = charNp + str; //+ "&#"+ str.charCodeAt(0) + ";";  //str.charCodeAt(0) disabled 20050604

                    } else if (str.charCodeAt(0) > "127") {
                        charNp = charNp + "&#" + str.charCodeAt(0) + ";";

                        // bold italic underline english ....

                    } else if (str1.length == 2 && str1.search(/\\/) == 0) {
                        charNp = charNp + str1.substring(1, 2); skip = 1; // escape char = embed english char  

                    } else if (str2 == "<b>" || str2 == "<i>" || str2 == "<u>" || str2 == "<p>") {
                        charNp = charNp + str2; skip = 2;

                    } else if (str4 == "</b>" || str4 == "</i>" || str4 == "</u>" || str4 == "<br>") {
                        charNp = charNp + str4; skip = 3;

                    } else if (str == "\n") {
                        charNp = charNp + "<br> \n";

                    } else if (str1 == "\_0") {
                        charNp = charNp + "॰"; skip = 1; // "&#2417;"

                    } else if (str.search(/\d/) == 0) {
                        charNp = charNp + numArrayHTML[str];

                    } else if (str.search(/\w/) >= 0) {
                        lowerCharPos = -1; upperCharPos = -1; synSkip = -1;







                        if (str.search(/[a-z]/) >= 0) {
                            var ichar = 0; ichar = str.charCodeAt(0) - 97; lowerCharPos = ichar;   // If lower char LOOP#11

                            //      var Achar="{"; APchar = Achar.charCodeAt(0) ; charNp = charNp + " " + APchar+ " "; //"{=123"

                            //







                            if (msg.length >= 1) {
                                str13 = msg.substring(0, 3);    // If lower char LOOP#12





                                if (str13.search("\{") == 0) {
                                    charNp = charNp + "र्" + charLCArrayHTML[lowerCharPos]; skip = 1;  // If lower char LOOP#13

                                } else if (str13.search("L{") == 0) {
                                    charNp = charNp + "र्" + charLCArrayHTML[lowerCharPos] + "ी"; skip = 2; //"&#2352;&#2381;"  //Long Ii-kar "&#2368;"; 

                                } else if (str13.search("l{") == 0) {
                                    charNp = charNp + "र्" + charLCArrayHTML[lowerCharPos] + "ि"; skip = 2; //"&#2352;&#2381;"  //Short Ii-kar "&#2367;"; 

                                } else if (str13.search("{") == 1) {

                                    //

                                        breakReplaceLC:

                                        for (var i = 0; i < refCharDCArray.length; i++) {
                                            if (str13.search(refCharDCArray[i]) == 0) {

                                                charNp = charNp + "र्" + charLCArrayHTML[lowerCharPos] + refCharDCArrayHTML[i]; skip = 2; synSkip = 1; //र ् "&#2352;&#2381;" 

                                                break breakReplaceLC;
                                            }
                                        } // end of FOR loop





                                    if (synSkip < 1) { charNp = charNp + charLCArrayHTML[lowerCharPos]; }  //opes and closes







                                } else if (str13.search("f]{") == 0) {
                                    charNp = charNp + "र्" + charLCArrayHTML[lowerCharPos] + "ो"; skip = 3;   //र ् "&#2352;&#2381;" //o-kar &#2379;

                                } else if (str13.search("f}{") == 0) {
                                    charNp = charNp + "र्" + charLCArrayHTML[lowerCharPos] + "ौ"; skip = 3;   //र ् "&#2352;&#2381;"  //ou-&#2380;





                                } else {
                                    if (str == "o") {   // If lower char LOOP#12's else and LOOP#14 begins





                                        if (str2.search("of]") == 0) {
                                            charNp = charNp + "ओ"; skip = 2; //"&#2323;"

                                        } else if (str2.search("ofS") == 0) {
                                            charNp = charNp + "ऑ"; skip = 2; //"&#2321;"; 

                                        } else if (str4.search("of]]") == 0) {
                                            charNp = charNp + "ऒ"; skip = 3; //"&#2322;"; // Short Aa+oKar "&#2322;"; 

                                        } else if (str2.search("of}") == 0) {
                                            charNp = charNp + "औ"; skip = 2; // "&#2324;"

                                        } else if (str1.search("of") == 0) {
                                            charNp = charNp + "आ"; skip = 1; // "&#2310;"

                                        } else { charNp = charNp + "अ"; } // "&#2309;";   //closed LOOP#15







                                    } else if (str1 == "p'" || str1 == "p\"") {
                                        charNp = charNp + "ऊ"; skip = 1; //"&#2314;";  //Start ELSEif for if loop[if (str == "o") {}]

                                    } else if (str == "p") {
                                        charNp = charNp + "उ"; //"&#2313;"

                                    } else if (str2 == "n[[") {
                                        charNp = charNp + "ॡ"; skip = 2; //Long Lree "&#2401;";  

                                    } else if (str1 == "n[") {
                                        charNp = charNp + "ऌ"; skip = 1; //Long Lree "&#2315;";  

                                    } else if (str4.search("f]]") == 1) {
                                        charNp = charNp + charLCArrayHTML[lowerCharPos] + "ॊ"; skip = 3; //"&#2378;"; // Short oKar "&#2378;"; 

                                    } else if (str2.search("]]") == 1) {
                                        charNp = charNp + charLCArrayHTML[lowerCharPos] + "ॆ"; skip = 2; // Short E-kar "&#2374;";  

                                    } else if (str2.search("f]") == 1) {
                                        charNp = charNp + charLCArrayHTML[lowerCharPos] + "ो"; skip = 2; //o-kar &#2379;

                                    } else if (str2.search("f}") == 1) {
                                        charNp = charNp + charLCArrayHTML[lowerCharPos] + "ौ"; skip = 2; //ou-&#2380;

                                    } else if (str1.search("f") == 1) {
                                        charNp = charNp + charLCArrayHTML[lowerCharPos] + "ा"; skip = 1;  //if (str == "o") ends. aa-kar &#2366;



                                    } else { charNp = charNp + charLCArrayHTML[lowerCharPos]; }
                                } //str13.search("{") if closed. // LOOP#14 closed LOOP#13 closed

                            } else { charNp = charNp + charLCArrayHTML[lowerCharPos]; } // msg.length <=1 case  //LOOP#12 closed







                            //

                        } else if (str.search(/[A-Z]/) >= 0) {  //Closing ELSE for if loop[if (str == "o") {}]  //LOOP#11 else if continues.

                            var ichar = 0; ichar = str.charCodeAt(0) - 65; upperCharPos = ichar;



                            if (msg.length >= 1) {
                                str13 = msg.substring(0, 3);  //LOOP#22 begins //LOOP#11 acts like LOOP#21





                                if (str1 == "O{") {
                                    charNp = charNp + "ई"; skip = 1; //"&#2312;" //LOOP#23 begins

                                } else if (str13.search("{") == 0) {
                                    charNp = charNp + "र्" + charUCArrayHTML[upperCharPos]; skip = 1;  //र ् "&#2352;&#2381;" 

                                } else if (str13.search("L{") == 0) {
                                    charNp = charNp + "र्" + charUCArrayHTML[upperCharPos] + "ी"; skip = 2; //long Ii-kar &#2368; //र ् "&#2352;&#2381;" 

                                } else if (str13.search("l{") == 0) {
                                    charNp = charNp + "र्" + charUCArrayHTML[upperCharPos] + "ि"; skip = 2; //Short Ii-kar &#2367; //र ् "&#2352;&#2381;" 

                                } else if (str13.search("{") == 1) {

                                    //  

                                        breakReplaceUC:

                                        for (var i = 0; i < refCharDCArray.length; i++) {

                                            if (str13.search(refCharDCArray[i]) == 0) {
                                                charNp = charNp + "र्" + charUCArrayHTML[upperCharPos] + refCharDCArrayHTML[i]; skip = 2; synSkip = 1; //"र ् &#2352;&#2381;"

                                                break breakReplaceUC;
                                            }
                                        }  //for opens and closes

                                    //

                                    if (synSkip < 1) { charNp = charNp + charUCArrayHTML[upperCharPos]; } //opens and closes





                                } else if (str13.search("f]{") == 0) {
                                    charNp = charNp + "र्" + charUCArrayHTML[upperCharPos] + "ो"; skip = 3; //"&#2379;"; //र ् "&#2352;&#2381;" 

                                } else if (str13.search("f}{") == 0) {
                                    charNp = charNp + "र्" + charUCArrayHTML[upperCharPos] + "ौ"; skip = 3; //"&#2380;";  //र ् "&#2352;&#2381;" 

                                } else {         //LOOP#23 continues:   //var refAaOuKarArray = new Array("Ol","OL","O{","O","P]]","P}","P","f]]","]]","f]","f}","I{"); 





                                    if (str1 == "Ol" || str1 == "OL" || str1 == "O{") {
                                        charNp = charNp + "ई"; skip = 1; //"ई&#2312;";  ////LOOP#24 begins

                                    } else if (str == "O") {
                                        charNp = charNp + "इ"; //"&इ#2311;";

                                    } else if (str2 == "P]]") {
                                        charNp = charNp + "ऎ"; skip = 2; //Shor Ai "&#2318;"; Long Ai "&#2320;"; 

                                    } else if (str1 == "P]" || str1 == "P}") {
                                        charNp = charNp + "ऐ"; skip = 1; //Ai "ऐ&#2320;"; 

                                    } else if (str == "P") {
                                        charNp = charNp + "ए";            // E--"ए&#2319;";

                                    } else if (str1 == "I[") {
                                        charNp = charNp + "ॠ"; skip = 1; //Long Rree "&#2400;";  

                                    } else if (str4.search("f]]") == 1) {
                                        charNp = charNp + charUCArrayHTML[upperCharPos] + "ॊ"; skip = 3; //"&#2378;"; // Short oKar "&#2378;"; 

                                    } else if (str2.search("]]") == 1) {
                                        charNp = charNp + charUCArrayHTML[upperCharPos] + "ॆ"; skip = 2; // Short E-kar "&#2374;";  

                                    } else if (str2.search("f]") == 1) {
                                        charNp = charNp + charUCArrayHTML[upperCharPos] + "ो"; skip = 2; //"&#2379;"; 

                                    } else if (str2.search("f}") == 1) {
                                        charNp = charNp + charUCArrayHTML[upperCharPos] + "ौ"; skip = 2; //"&#2380;"; 

                                    } else if (str1.search("f") == 1) {
                                        charNp = charNp + charUCArrayHTML[upperCharPos] + "ा"; skip = 1;  //aa-kar "&#2366;";  //Short i-kar "&#2367;";  

                                    } else { charNp = charNp + charUCArrayHTML[upperCharPos]; }
                                } //LOOP#24 LOOP#23 closed

                            } else { charNp = charNp + charUCArrayHTML[upperCharPos]; }  //LOOP#22 closes









                            // if other than alphabetical characters: i.e. if non-alphanumerica chars.

                        } else { if (str4 == "_n[[") { charNp = charNp + "ॣ"; skip = 3 } else if (str2 == "_n[") { charNp = charNp + "ॢ"; skip = 2 }; charNp = charNp + str; } //LOOP#11 continues //Under Lri "&#2402;"; (Check this part.)//Under Lree "&#2403;";  





                    } else if (str == ";") {

                        if (str4.search("f]]") == 1) {
                            charNp = charNp + "स" + "ॊ"; skip = 3; //"&#2378;"; // Short oKar "&#2378;"; 

                        } else if (str2.search("]]") == 1) {
                            charNp = charNp + "स" + "ॆ"; skip = 2; // Short E-kar "&#2374;";  

                        } else if (str2.search("f]") == 1) {
                            charNp = charNp + "स" + "ो"; skip = 2; //"&#2379;"; 

                        } else if (str2.search("f}") == 1) {
                            charNp = charNp + "स" + "ौ"; skip = 2; //"&#2380;"; 

                        } else if (str1.search("f") == 1) {
                            charNp = charNp + "स" + "ा"; skip = 1;  //aa-kar "&#2366;";  //Short i-kar "&#2367;";  

                        } else { charNp = charNp + "स"; }





                    } else if (str == "{") {
                        charNp = charNp + "्र"; //"्र&#2352;&#2381;" 









                    } else {  // continues after if globalSkip loop ends. //LOOP#11 continues



                        breakEspChar:

                            for (var ichar = 0; ichar < refEspCharArray.length; ichar++) {
                                lSkip = 0; lSkip = refEspCharASKIP[ichar];

                                if (lSkip == 1) {
                                    if (str == refEspCharArray[ichar]) { charNp = charNp + refEspCharAHTML[ichar]; globalSkip = 1; break breakEspChar; }

                                } else if (lSkip == 2) {
                                    if (str1 == refEspCharArray[ichar]) { charNp = charNp + refEspCharAHTML[ichar]; skip = 1; globalSkip = 1; break breakEspChar; }

                                } else if (lSkip == 3) { if (str2 == refEspCharArray[ichar]) { charNp = charNp + refEspCharAHTML[ichar]; skip = 2; globalSkip = 1; break breakEspChar; } } else { globalSkip = 0; } // else if closed.

                            }          // for loop closed.



                        if (globalSkip == 0) { charNp = charNp + str; }
                    };
                };
            } else { };



            prnCharNpLen = charNp.length;

            return charNp;

            msgText = charNp + msgLastWord; if (myCode == 1) { document.writeln("<HTML><BODY>" + msgText + "</BODY></HTML>"); }



        }
    } // endChar and myEditedTextPart 's if loops

}




function setUnicode(e, field) {
    /*
		unicodeSajha ==N Enable Sajha font css
		unicodeSajha ==Y Enable Unicode
	
	*/
    if (unicodeSajha == "N")
        return true;
    var unicode = e.charCode ? e.charCode : e.keyCode;

    var check = true;
    if (browser == "Netscape" && e.keyCode != 0)
        check = false;

    if (check) {
        new_value = spRckFoc(0, String.fromCharCode(unicode));
        if (new_value == "")
            new_value = String.fromCharCode(unicode);

        insertAtCursor(field, new_value);
        return false //disable key press		
    }
}

function insertAtCursor(myField, myValue) {
    //IE support
    if (document.selection) {
        myField.focus();
        sel = document.selection.createRange();
        sel.text = myValue;
        sel.select();
    }
        //MOZILLA/NETSCAPE support
    else if (myField.selectionStart || myField.selectionStart == '0') {
        var startPos = myField.selectionStart;
        var endPos = myField.selectionEnd;
        var newEndPos = startPos + myValue.length;
        myField.value = myField.value.substring(0, startPos) + myValue + myField.value.substring(endPos, myField.value.length);
        myField.setSelectionRange(newEndPos, newEndPos);
    }
    else {
        var newEndPos = myField.value.length + myValue.length;
        myField.value += myValue;
        myField.setSelectionRange(newEndPos, newEndPos);
    }
}

$('.nep').live('keypress', function (e) {
    //alert(this.name);
    return setUnicode(e, this);
});
/* -- End Hiding Here --> */