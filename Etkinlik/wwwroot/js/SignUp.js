function regexNick(nickname) {
	var reg = /^[a-zA-Z]{5,25}\w*$/;
	return reg.test(String(nickname));
}

function regexName(fullName) {
	var reg = /^[a-zA-Z]{5,25}\w*$/;
	return reg.test(String(fullName));
}

function regexPassword(password) {
	var reg = /^[a-zA-Z]{5,25}\w*$/;
	return reg.test(String(password));
}

function regexEmail(email) {
    var reg = /^(([^<>()\[\]\\.,;:\s@"]+(\.[^<>()\[\]\\.,;:\s@"]+)*)|(".+"))@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+[a-zA-Z]{2,}))$/;
    return reg.test(String(email).toLowerCase());
}

let validateBox = function(field, funcName) {
	field.on("keyup", function() {
		var bool = funcName(field.val());
		console.log(bool);
	});
}

$("#button").on.