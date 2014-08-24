$ ->
    $(document).ready ()->    
        blockNumber = 2;
        NoMoreData = false;
        inProgress = false;
        $(window).scroll () ->
            if $(window).scrollTop() == $(document).height() - $(window).height() && !NoMoreData && !inProgress 
                inProgress = true
                $("#loadingDiv").show()
                $.post "/Exercise/InfinateScroll", { "blockNumber": blockNumber, "id": $('#Id')[0].value },
                    (data) ->
                        blockNumber = blockNumber + 1
                        NoMoreData = data.NoMoreData
                        $("#bookListDiv").append(data.HTMLString)
                        $("#loadingDiv").hide()
                        inProgress = false