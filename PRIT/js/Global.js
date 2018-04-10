debugger;
//var loadingMessageText = '<img src="' + baseURL + '/loading.gif" alt="" /> Loading... ';
var isToDisableBusyModel = false;


// HIDE BUSY MODAL & LOADING MESSAGE ON PAGE READY
hideBusyModal();



$(function () {
    debugger;
    $('a:not([href*="mailto:"],[href*="#"],[href*="javascript:void"],[target=_blank],[rel*=lightbox],.close-reveal-modal,.saleType)').click(function (event, element) {
        debugger;
		if (event.which == 1/*LEFT CLICK*/) {
			showBusyModal();
			ShowLoadingMessage();
		}
	});

	// MVC Post
	$("form").on("submit", ":not([target='_blank'])", function () {
		showBusyModal();
	});

	//$(".numeric").keypress(function (e) {
	//	if (e.which != 8 && e.which != 0 && (e.which < 48 || e.which > 57)) {
	//		return false;
	//	}
	//}).blur(function (e) {
	//	if (isNaN($(this).val()))
	//		$(this).val('');
	//	else if ($(this).val() != '') {			
	//	}
	//});

	hideBusyModal();

});

//$.ajaxPrefilter(function (options, originalOptions, jqXHR) {
//    options.async = true;
//});

var flag = 0;
var globalAjaxFlag = 0
$.ajaxSetup({ global: true, cache: false });

$(document)
    .ajaxStart(function () {
        debugger;
		onAjaxStart();
	})
    .ajaxStop(function () {
        debugger;
		onAjaxStop();
	})
	.ajaxError(
	function (e, request, settings, exception) {
        debugger;
		hideBusyModal();
		if (request.status == 403) {
			window.location.href = window.location.href;
			return;
		}
	});




function onAjaxStart() {
	showBusyModal();

	if (flag == 0) {
		globalAjaxFlag = 1;
		ShowLoadingMessage();
	}
}

function onAjaxStop() {
	hideBusyModal();
	if (flag == 0 && globalAjaxFlag == 1) {
		
		globalAjaxFlag = 0;
	
	}
}

// SHOW LOADING MESSAGE FOR Non-ASYNC Ajax calls.
function NonAsyncAjaxStart() {
    debugger;
	showBusyModal();
	ShowLoadingMessage();
}

//HIDE LOADING MESSAGE FOR Non-ASYNC Ajax calls.
function NonAsyncAjaxStop() {
    debugger;
	hideBusyModal();

}

function ShowLoadingMessage() {
    $("#divLoader").show();
  
}


//function openNewDialog(hrefPath) {
//	$("#editModal-Content")
//		.html('')
//		.load(hrefPath, function () {
//			$("#editModal").reveal();
//			$("#editModal").find('input,select').not(':hidden,[disabled=disabled],[readonly=readonly]').first().focus();
//		});
//}

//function closeModal(modal) {
//	$("#" + modal).trigger('reveal:close');
//	$("#editModal-Content").empty();
//}


function showBusyModal() {
	if (!isToDisableBusyModel) {
        $('#divLoader').show();
        //$('#hhh').fadeTo("slow", 0.33);

        //$('html, body').css('opacity', '0.8'); 

       // $('body').css('background', 'none 0px 0px repeat scroll rgba(0, 0, 0, 0.7)'); 

        //background: none 0px 0px repeat scroll rgba(0, 0, 0, 0.7);
	}
	isToDisableBusyModel = false;
}

function hideBusyModal() {
	//$('.reveal-modal-bg1').hide();
    $('#divLoader').hide();

}
