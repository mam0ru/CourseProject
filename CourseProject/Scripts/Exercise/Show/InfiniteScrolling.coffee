$ ->
    $(document).ready ()->    
        blockNumber = 2;
        $("#InfinityBlockNumber")[0].value = blockNumber
        inProgress = false;
        $(window).scroll () ->
            if $(window).scrollTop() == $(document).height() - $(window).height() && !inProgress 
                blockNumber = parseInt($("#InfinityBlockNumber")[0].value)
                inProgress = true
                $("#loadingDiv").show()
                $.post "/Exercise/InfinateScroll", { "blockNumber": blockNumber, "id": $('#Id')[0].value },
                    (data) ->
                        blockNumber = blockNumber + 1
                        $("#commentsList").append(data.HTMLString)
                        $("#loadingDiv").hide()
                        $("#InfinityBlockNumber")[0].value = blockNumber
                        inProgress = false